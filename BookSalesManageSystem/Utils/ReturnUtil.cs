using BookSalesManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class ReturnUtil
    {
        // 添加
        public static void AddReturn(Return @return)
        {
            using (var statement = SqlUtil.conn.Prepare("INSERT INTO return_book (b_id,, r_time, number, total_price) VALUES (?, ?, ?, ?)"))
            {
                statement.Bind(1, @return.Book.BId);
                statement.Bind(2, @return.Time);
                statement.Bind(3, @return.Number);
                statement.Bind(4, @return.TotalPrice);
                statement.Step();
            }
        }

        // 得到全部(order by time)
        public static List<Return> GetAllReturns()
        {
            return null;
        }
    }
}
