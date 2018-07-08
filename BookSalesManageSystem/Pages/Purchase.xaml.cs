using BookSalesManageSystem.Models;
using BookSalesManageSystem.Utils;
using BookSalesManageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Purchase : Page
    {
        ObservableCollection<SupplierStock> supplierStocks = new ObservableCollection<SupplierStock>();
        SupplierStock select = null;

        public Purchase()
        {
            this.InitializeComponent();
        }

        private void SearchId_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BookIDBox.Text))
                return;
            BookDetail.Visibility = Visibility.Visible;
            supplierStocks.Clear();
            SupplierStockUtil.QuerySupplierStock(int.Parse(BookIDBox.Text)).ForEach(p => supplierStocks.Add(p));
            if (supplierStocks.Count() > 0)
                select = supplierStocks[0];
            else
                BookDetail.Visibility = Visibility.Collapsed;
        }

        private async void Sure_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BookIDBox.Text) || string.IsNullOrEmpty(SaleNumberBox.Text) || string.IsNullOrEmpty(SalePriceBox.Text))
                return;
            if(select != null)
            {
                int n = int.Parse(SaleNumberBox.Text);
                float n2 = float.Parse(SalePriceBox.Text);
                SaleNumberBox.Text = "";
                SalePriceBox.Text = "";
                if (n > select.Number)
                {
                    await new MessageDialog("这种书没有多么多了，滚！").ShowAsync();
                    return;
                }
                // 库存记录
                if (StockUtil.QueryStock(select.Book.BId.ToString()) == null)
                    StockViewModel.GetInstance().AddStock(new Models.Stock { Book = select.Book, Number = 0, OfferPrice = select.Price, SalePrice = n2 });
                StockViewModel.GetInstance().UpdateStock(select.Book.BId, n);
                SupplierStockUtil.UpdateSupplierStock(select.Supplier.SId, select.Book.BId, 0 - n);
                // 进货记录
                Models.Purchase purchase = new Models.Purchase { Book = select.Book, Number = n, Time = DateTimeOffset.Now, Supplier = select.Supplier, Price = select.Price };
                PurchaseUtil.AddPurchase(purchase);
                await new MessageDialog("进货成功了！").ShowAsync();
            }
            else
                await new MessageDialog("没有这种书供应，请重新输入书籍编号！").ShowAsync();
            BookIDBox.Text = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            BookIDBox.Text = "";
            SaleNumberBox.Text = "";
            SalePriceBox.Text = "";
        }

        private void SupplierList_ItemClick(object sender, ItemClickEventArgs e)
        {
            select = (SupplierStock)e.ClickedItem;
        }

        private void BookIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookIDLabel.Visibility = string.IsNullOrEmpty(BookIDBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            if (string.IsNullOrEmpty(BookIDBox.Text) && BookDetail.Visibility == Visibility.Visible)
                BookDetail.Visibility = Visibility.Collapsed;
        }

        private void SaleNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaleNumberLabel.Visibility = string.IsNullOrEmpty(SaleNumberBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SalePriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SalePriceLabel.Visibility = string.IsNullOrEmpty(SalePriceBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
