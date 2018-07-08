using BookSalesManageSystem.Models;
using BookSalesManageSystem.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Statistics : Page
    {
        ObservableCollection<MonthSales> monthSales = new ObservableCollection<MonthSales>();
        ObservableCollection<Sale> monthSalesDetail = new ObservableCollection<Sale>();
        ObservableCollection<Sale> sales = new ObservableCollection<Sale>();
        ObservableCollection<Models.Return> returns = new ObservableCollection<Models.Return>();
        ObservableCollection<Models.Purchase> purchases = new ObservableCollection<Models.Purchase>();

        public Statistics()
        {
            this.InitializeComponent();
            // MonthSales month = new MonthSales { Month = 7, TotalSaleNum = 20, TotalSales = 34.5f };
            // monthSales.Add(month);
            // Sale sale = new Sale { Book = new Book { BId = 1, BAuthor = "a", BName = "b" }, Number = 12, TotalPrice = 23.4f };
            // monthSalesDetail.Add(sale);
            SalesUtil.GetAllMonthSales().ForEach(p => monthSales.Add(p));
            SalesUtil.GetAllSales().ForEach(p => sales.Add(p));
            ReturnUtil.GetAllReturns().ForEach(p => returns.Add(p));
            PurchaseUtil.GetAllPurchases().ForEach(p => purchases.Add(p));
        }

        private void ShowCharts_Click(object sender, RoutedEventArgs e)
        {
            monthSalesDetail.Clear();
            var s = sender as FrameworkElement;
            SalesUtil.QuerySale(((MonthSales)s.DataContext).Month).ForEach(p => monthSalesDetail.Add(p));
            MyPivot.SelectedIndex = 1;
        }
    }
}
