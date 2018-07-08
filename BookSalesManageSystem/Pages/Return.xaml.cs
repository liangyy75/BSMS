using BookSalesManageSystem.Utils;
using BookSalesManageSystem.ViewModels;
using System;
using System.Collections.Generic;
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
    public sealed partial class Return : Page
    {
        public Return()
        {
            this.InitializeComponent();
        }

        private async void Sure_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BookIDBox.Text) || string.IsNullOrEmpty(SaleNumberBox.Text))
                return;
            Models.Stock stock = StockUtil.QueryStock(BookIDBox.Text);
            if (stock != null)
            {
                int n = int.Parse(SaleNumberBox.Text);
                SaleNumberBox.Text = "";
                if (n < 0)
                {
                    await new MessageDialog("还书的数值不正确！").ShowAsync();
                    return;
                }
                // 库存记录
                StockViewModel.GetInstance().UpdateStock(stock.Book.BId, n);
                // 还书记录
                Models.Return @return = new Models.Return { Book = stock.Book, Number = n, Time = DateTimeOffset.Now, TotalPrice = stock.SalePrice * n };
                ReturnUtil.AddReturn(@return);
                await new MessageDialog("归还成功！").ShowAsync();
            }
            else
                await new MessageDialog("本店没有进过这种书，不必归还到此！").ShowAsync();
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
        }

        private void SaleNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaleNumberLabel.Visibility = string.IsNullOrEmpty(SaleNumberBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
