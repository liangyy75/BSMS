using BookSalesManageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace BookSalesManageSystem.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Stock : Page
    {
        StockViewModel stockView = StockViewModel.GetInstance();

        public Stock()
        {
            this.InitializeComponent();
            // Book book = new Book() { BId = 1, BName="smg", BAuthor="wtf" };
            // Models.Stock stock = new Models.Stock() { Book=book, Number=10, OfferPrice=1.5f, SalePrice=2.0f };
            // stocks.Add(stock);
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text))
            {
                stockView.ReGetAllStocks();
                return;
            }
            stockView.SaveStocks();
            stockView.QueryStocks(sender.Text);
        }
    }
}
