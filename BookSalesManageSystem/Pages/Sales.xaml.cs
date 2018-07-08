using BookSalesManageSystem.Utils;
using BookSalesManageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Sales : Page
    {
        public Sales()
        {
            this.InitializeComponent();
        }

        private void SearchId_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BookIDBox.Text))
                return;
            Models.Stock stock = StockUtil.QueryStock(BookIDBox.Text);
            if (stock != null)
            {
                BookDetail.Visibility = Visibility.Visible;
                BookId.Text = stock.Book.BId.ToString();
                BookName.Text = stock.Book.BName;
                BookAuthor.Text = stock.Book.BAuthor;
                BookNumber.Text = stock.Number.ToString();
                BookSalePrice.Text = stock.SalePrice.ToString();
                BookBuyPrice.Text = stock.OfferPrice.ToString();
            }
            else
                BookDetail.Visibility = Visibility.Collapsed;
        }

        private async void Sure_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SaleNumberBox.Text) || string.IsNullOrEmpty(BookIDBox.Text))
                return;
            Models.Stock stock = StockUtil.QueryStock(BookIDBox.Text);
            if (stock != null)
            {
                int n = int.Parse(SaleNumberBox.Text);
                SaleNumberBox.Text = "";
                if (n > stock.Number)
                {
                    await new MessageDialog("这种书没有多么多了！").ShowAsync();
                    return;
                }
                // 库存记录
                StockViewModel.GetInstance().UpdateStock(stock.Book.BId, 0 - n);
                // 销售记录
                Models.Sale sale = new Models.Sale { Book = stock.Book, Number = n, Time = DateTimeOffset.Now, TotalPrice = n * stock.SalePrice };
                SalesUtil.AddSale(sale);
                await new MessageDialog("销售成功！").ShowAsync();
            }
            else
                await new MessageDialog("没有这种书，请重新输入书籍编号！").ShowAsync();
            BookIDBox.Text = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            BookIDBox.Text = "";
            SaleNumberBox.Text = "";
        }

        private void BookIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookIDLabel.Visibility = string.IsNullOrEmpty(BookIDBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            if (string.IsNullOrEmpty(BookIDBox.Text) && BookDetail.Visibility == Visibility.Collapsed)
                BookDetail.Visibility = Visibility.Collapsed;
        }

        private void SaleNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaleNumberLabel.Visibility = string.IsNullOrEmpty(SaleNumberBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
