using BookSalesManageSystem.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class BookUtil
    {
        // 查询
        public static Book QueryBook(int id)
        {
            Book book = null;
            using (var statement = SqlUtil.conn.Prepare("SELECT b_id, b_name, b_author FROM book WHERE b_id = ?"))
            {
                statement.Bind(1, id);
                if (SQLiteResult.ROW == statement.Step())
                {
                    book = new Book { BId = int.Parse(statement[0].ToString()), BName = (string)statement[1], BAuthor = (string)statement[2] };
                }
            }
            return book;
        }
    }
}
