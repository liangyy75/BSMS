using BookSalesManageSystem.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class MonthSalesUtil
    {
        // 添加
        public static void AddMonthSales(MonthSales monthSales)
        {
            
        }

        // 得到全部(order by time)
        public static List<MonthSales> GetAllMonthSales()
        {
            string sql = "select sum(number), sum(total_price), strftime(\"%m\", s_time) as month from sale group by month";
            var conn = SqlUtil.conn;
            List<MonthSales> monthSaleses = new List<MonthSales>();
            using(var statement = conn.Prepare(sql))
            {
                while(statement.Step() == SQLiteResult.ROW)
                {
                    MonthSales monthSales = new MonthSales();
                    monthSales.TotalSaleNum = Int32.Parse(statement[0].ToString());
                    monthSales.TotalSales = float.Parse(statement[1].ToString());
                    monthSales.Month = int.Parse(statement[2].ToString());
                    Debug.WriteLine(statement[2].ToString());
                    monthSaleses.Add(monthSales);
                }
            }
            return monthSaleses;
        }
    }
}
