using BookSalesManageSystem.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class SalesUtil
    {
        // 添加
        public static void AddSale(Sale sale)
        {
            using (var statement = SqlUtil.conn.Prepare("INSERT INTO sale (b_id, number, total_price, s_time) VALUES (?, ?, ?, ?)"))
            {
                statement.Bind(1, sale.Book.BId);
                statement.Bind(2, sale.Number);
                statement.Bind(3, sale.TotalPrice);
                statement.Bind(4, sale.Time.ToString("u"));
                statement.Step();
            }
        }

        // 得到全部(order by time)
        public static List<Sale> GetAllSales()
        {
            string sql = "select s.s_time, s.number, s.total_price, b.b_id, b.b_name, b.b_author from sale s, book b where b.b_id=s.b_id order by s.s_time";
            var conn = SqlUtil.conn;
            List<Sale> sales = new List<Sale>();
            using (var statement = conn.Prepare(sql))
            {
                while(statement.Step() == SQLitePCL.SQLiteResult.ROW)
                {
                    Sale sale = new Sale();
                    sale.Time = DateTimeOffset.Parse(statement[0].ToString());
                    sale.Number = int.Parse(statement[1].ToString());
                    sale.TotalPrice = float.Parse(statement[2].ToString());
                    sale.Book = new Book();
                    sale.Book.BId = int.Parse(statement[3].ToString());
                    sale.Book.BName = statement[4].ToString();
                    sale.Book.BAuthor = statement[5].ToString();
                    sales.Add(sale);

                }
            }
            return sales;
        }

        // 查询(要查到对应的月份的所有书籍并根据他们销量排序)
        public static List<Sale> QuerySale(int month)
        {
            string sql = "select s.s_time, sum(s.number) total, sum(s.total_price), b.b_id, b.b_name, b.b_author from sale s, book b where b.b_id=s.b_id and strftime(\"%m\", s.s_time) = ? group by b.b_id order by total desc";
            var conn = SqlUtil.conn;
            List<Sale> sales = new List<Sale>();
            using (var statement = conn.Prepare(sql))
            {
                string mon = month > 9 ? month.ToString() : "0" + month;
                statement.Bind(1, mon);
                while(statement.Step() == SQLitePCL.SQLiteResult.ROW)
                {
                    Sale sale = new Sale();
                    sale.Time = DateTimeOffset.Parse(statement[0].ToString());
                    sale.Number = int.Parse(statement[1].ToString());
                    sale.TotalPrice = float.Parse(statement[2].ToString());
                    sale.Book = new Book();
                    sale.Book.BId = int.Parse(statement[3].ToString());
                    sale.Book.BName = statement[4].ToString();
                    sale.Book.BAuthor = statement[5].ToString();
                    sales.Add(sale);
                }
            }
            return sales;
        }

        // 得到全部(order by time)
        public static List<MonthSales> GetAllMonthSales()
        {
            string sql = "select sum(number), sum(total_price), strftime(\"%m\", s_time) as month from sale group by month order by month desc";
            var conn = SqlUtil.conn;
            List<MonthSales> monthSaleses = new List<MonthSales>();
            using (var statement = conn.Prepare(sql))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    MonthSales monthSales = new MonthSales();
                    monthSales.TotalSaleNum = Int32.Parse(statement[0].ToString());
                    monthSales.TotalSales = float.Parse(statement[1].ToString());
                    monthSales.Month = int.Parse(statement[2].ToString());
                    monthSaleses.Add(monthSales);
                }
            }
            return monthSaleses;
        }
    }
}
