using BookSalesManageSystem.Pages;
using BookSalesManageSystem.Utils;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace BookSalesManageSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string[] frames = { "库存", "退货", "进货", "统计", "销售"};

        public MainPage()
        {
            this.InitializeComponent();
            MyContent.Navigate(typeof(Sales));
            SqlUtil.LoadDatabase();
        }

        private void Navigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            string str = (string)e.ClickedItem;
            switch (str)
            {
                case "退货": MyContent.Navigate(typeof(Return)); break;
                case "进货": MyContent.Navigate(typeof(Purchase)); break;
                case "统计": MyContent.Navigate(typeof(Statistics)); break;
                case "销售": MyContent.Navigate(typeof(Sales)); break;
                case "库存": MyContent.Navigate(typeof(Stock)); break;
            }
        }
    }
}
