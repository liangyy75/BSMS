using BookSalesManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class PurchaseUtil
    {
        // 添加
        public static void AddPurchase(Purchase purchase)
        {
            using (var statement = SqlUtil.conn.Prepare("INSERT INTO purchase (s_id, b_id, number, price, p_time) VALUES (?, ?, ?, ?, ?)"))
            {
                statement.Bind(1, purchase.Supplier.SId);
                statement.Bind(2, purchase.Book.BId);
                statement.Bind(3, purchase.Number);
                statement.Bind(4, purchase.Price);
                statement.Bind(5, purchase.Time);
                statement.Step();
            }
        }

        // 得到全部(order by time)
        public static List<Purchase> GetAllPurchases()
        {
            return null;
        }
    }
}
