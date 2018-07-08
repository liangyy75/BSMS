using BookSalesManageSystem.Models;
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
                statement.Bind(4, sale.Time);
                statement.Step();
            }
        }

        // 得到全部(order by time)
        public static List<Sale> GetAllSales()
        {
            return new List<Sale>();
        }

        // 查询(要查到对应的月份的所有书籍并根据他们销量排序)
        public static List<Sale> QuerySale(int month)
        {
            return new List<Sale>();
        }
    }
}
