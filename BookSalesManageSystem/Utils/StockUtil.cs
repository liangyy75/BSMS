using BookSalesManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class StockUtil
    {
        // 添加
        public static void AddStock(Stock stock)
        {
            using (var statement = SqlUtil.conn.Prepare("INSERT INTO stock (b_id, number, offer_price, sale_price) VALUES (?, ?, ?, ?)"))
            {
                statement.Bind(1, stock.Book.BId);
                statement.Bind(2, stock.Number);
                statement.Bind(3, stock.OfferPrice);
                statement.Bind(4, stock.SalePrice);
                statement.Step();
            }
        }

        // 更改
        public static void UpdateStock(int id, int number)
        {
            using (var statement = SqlUtil.conn.Prepare("UPDATE stock SET number = number + ? WHERE b_id = ?"))
            {
                statement.Bind(1, number);
                statement.Bind(2, id);
                statement.Step();
            }
        }

        // 得到全部
        public static List<Stock> GetAllStocks()
        {
            return null;
        }

        // 查询
        public static Stock QueryStock(string id)
        {
            return null;
        }

        // 模糊查询
        public static List<Stock> QueryStocks(string str)
        {
            return null;
        }
    }
}
