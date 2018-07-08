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
            string sql = "select p.p_time, p.price, p.number, s.s_id, s.s_name, b.b_id, b.b_name, b.b_author from purchase p, supplier s, book b where p.s_id=s.s_id and p.b_id=b.b_id order by p.p_time";
            var conn = SqlUtil.conn;
            List<Purchase> purchases = new List<Purchase>();
            using (var statement = conn.Prepare(sql))
            {
                while(statement.Step() == SQLitePCL.SQLiteResult.ROW)
                {
                    Purchase purchase = new Purchase();
                    purchase.Time = DateTimeOffset.Parse(statement[0].ToString());
                    purchase.Price = float.Parse(statement[1].ToString());
                    purchase.Number = int.Parse(statement[2].ToString());
                    purchase.Supplier = new Supplier();
                    purchase.Supplier.SId = int.Parse(statement[3].ToString());
                    purchase.Supplier.SName = statement[4].ToString();
                    purchase.Book = new Book();
                    purchase.Book.BId = int.Parse(statement[5].ToString());
                    purchase.Book.BName = statement[6].ToString();
                    purchase.Book.BAuthor = statement[7].ToString();
                    purchases.Add(purchase);
                }
            }
            return purchases;
        }
    }
}
