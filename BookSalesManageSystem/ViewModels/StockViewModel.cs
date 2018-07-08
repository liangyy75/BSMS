using BookSalesManageSystem.Models;
using BookSalesManageSystem.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.ViewModels
{
    public class StockViewModel
    {
        ObservableCollection<Models.Stock> allStocks = new ObservableCollection<Models.Stock>();
        public ObservableCollection<Models.Stock> stocks = new ObservableCollection<Models.Stock>();

        private static StockViewModel _instance;
        public static StockViewModel GetInstance()
        {
            return _instance ?? (_instance = new StockViewModel());
        }

        private StockViewModel()
        {
            StockUtil.GetAllStocks().ForEach(p => stocks.Add(p));
        }

        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            StockUtil.AddStock(stock);
        }

        public void UpdateStock(int booid, int number)
        {
            for (int i = 0; i < stocks.Count(); i++)
                if (stocks[i].Book.BId == booid)
                    stocks[i].Number += number;
            StockUtil.UpdateStock(booid, number);
        }

        public void SaveStocks()
        {
            if (allStocks.Count() == 0 && stocks.Count() > 0)
            {
                foreach (Stock stock in stocks)
                    allStocks.Add(stock);
                stocks.Clear();
            }
        }

        public void ReGetAllStocks()
        {
            if(allStocks.Count() > 0)
            {
                stocks.Clear();
                foreach (Stock stock in allStocks)
                    stocks.Add(stock);
                allStocks.Clear();
            }
        }

        public void QueryStocks(string str)
        {
            stocks.Clear();
            StockUtil.QueryStocks(str).ForEach(p => stocks.Add(p));
        }
    }
}
