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
            using (var statement = SqlUtil.conn.Prepare("INSERT INTO return_book (b_id, r_time, number, total_price) VALUES (?, ?, ?, ?)"))
            {
                statement.Bind(1, @return.Book.BId);
                statement.Bind(2, @return.Time.ToString("u"));
                statement.Bind(3, @return.Number);
                statement.Bind(4, @return.TotalPrice);
                statement.Step();
            }
        }

        // 得到全部(order by time)
        public static List<Return> GetAllReturns()
        {
            string sql = "select r.r_time, r.number, r.total_price, b.b_id, b.b_name, b.b_author from return_book r, book b where b.b_id = r.b_id order by r.r_time";
            var conn = SqlUtil.conn;
            List<Return> returns = new List<Return>();
            using(var statement = conn.Prepare(sql))
            {
                while (statement.Step() == SQLitePCL.SQLiteResult.ROW)
                {
                    Return r = new Return();
                    r.Time = DateTimeOffset.Parse(statement[0].ToString());
                    r.Number = int.Parse(statement[1].ToString());
                    r.TotalPrice = float.Parse(statement[2].ToString());
                    r.Book = new Book();
                    r.Book.BId = int.Parse(statement[3].ToString());
                    r.Book.BName = statement[4].ToString();
                    r.Book.BAuthor = statement[5].ToString();
                    returns.Add(r);
                }
            }
            return returns;
        }
    }
}
