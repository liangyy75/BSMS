using BookSalesManageSystem.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class SupplierUtil
    {
        // 查询
        public static Supplier QuerySupplier(int id)
        {
            Supplier supplier = null;
            using (var statement = SqlUtil.conn.Prepare("SELECT s_id, s_name FROM supplier WHERE s_id = ?"))
            {
                statement.Bind(1, id);
                if (SQLiteResult.ROW == statement.Step())
                {
                    supplier = new Supplier { SId = int.Parse(statement[0].ToString()), SName = (string)statement[1] };
                }
            }
            return supplier;
        }
    }
}
