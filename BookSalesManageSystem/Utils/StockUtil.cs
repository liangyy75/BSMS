using BookSalesManageSystem.Models;
using SQLitePCL;
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
            List<Stock> stocks = new List<Stock>();
            using (var statement = SqlUtil.conn.Prepare(
                "SELECT S.b_id, B.b_name, B.b_author, S.number, S.offer_price, S.sale_price " +
                "FROM stock S, book B " +
                "WHERE S.b_id = B.b_id"))
            {
                while (SQLiteResult.ROW == statement.Step())
                {
                    Book book = new Book { BId = int.Parse(statement[0].ToString()), BName = (string)statement[1], BAuthor = (string)statement[2] };
                    Stock stock = new Stock { Book = book, Number = int.Parse(statement[3].ToString()), OfferPrice = float.Parse(statement[4].ToString()), SalePrice = float.Parse(statement[5].ToString()) };
                    stocks.Add(stock);
                }
            }
            return stocks;
        }

        // 查询
        public static Stock QueryStock(string id)
        {
            Stock stock = null;
            using (var statement = SqlUtil.conn.Prepare(
                "SELECT S.b_id, B.b_name, B.b_author, S.number, S.offer_price, S.sale_price " +
                "FROM stock S, book B " +
                "WHERE S.b_id = ? and S.b_id = B.b_id"))
            {
                statement.Bind(1, id);
                if (SQLiteResult.ROW == statement.Step())
                {
                    Book book = new Book { BId = int.Parse(statement[0].ToString()), BName = (string)statement[1], BAuthor = (string)statement[2] };
                    stock = new Stock { Book = book, Number = int.Parse(statement[3].ToString()), OfferPrice = float.Parse(statement[4].ToString()), SalePrice = float.Parse(statement[5].ToString()) };
                }
            }
            return stock;
        }

        // 模糊查询
        public static List<Stock> QueryStocks(string str)
        {
            List<Stock> stocks = new List<Stock>();
            using (var statement = SqlUtil.conn.Prepare(
                "SELECT S.b_id, B.b_name, B.b_author, S.number, S.offer_price, S.sale_price " +
                "FROM stock S, book B " +
                "WHERE S.b_id = B.b_id and (S.b_id LIKE ? or B.b_name LIKE ? or B.b_author LIKE ?)"))
            {
                string searchStr = "%" + str + "%";
                statement.Bind(1, searchStr);
                statement.Bind(2, searchStr);
                statement.Bind(3, searchStr);
                while (SQLiteResult.ROW == statement.Step())
                {
                    Book book = new Book { BId = int.Parse(statement[0].ToString()), BName = (string)statement[1], BAuthor = (string)statement[2] };
                    Stock stock = new Stock { Book = book, Number = int.Parse(statement[3].ToString()), OfferPrice = float.Parse(statement[4].ToString()), SalePrice = float.Parse(statement[5].ToString()) };
                    stocks.Add(stock);
                }
            }
            return stocks;
        }
    }
}
