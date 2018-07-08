using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BookSalesManageSystem.Utils
{
    public class SqlUtil
    {
        public static SQLiteConnection conn;

        // 建表
        public static void LoadDatabase()
        {
            conn = new SQLiteConnection("BSMS.db");
            
            create_table(@" CREATE TABLE IF NOT EXISTS book(
	                b_id integer  PRIMARY KEY AUTOINCREMENT,
	                b_name VARCHAR(20),
	                b_author VARCHAR(10)
                );");
            create_table(@" CREATE TABLE IF NOT EXISTS stock (
	                b_id integer  ,
	                number integer  ,
	                offer_price REAL,
	                sale_price REAL,
	                PRIMARY KEY(b_id),
	                FOREIGN KEY(b_id) REFERENCES book(b_id)
                );");
            create_table(@"
                CREATE TABLE IF NOT EXISTS sale(
	                b_id integer  ,
	                number integer, 
	                total_price REAL,
	                s_time DATETIME,
	                FOREIGN KEY(b_id) REFERENCES book(b_id),
	                PRIMARY KEY(b_id, s_time)
                );");
            create_table(@"
                CREATE TABLE IF NOT EXISTS return_book (
	                b_id integer  ,
	                r_time DATETIME,
	                number integer  ,
	                total_price REAL  ,
	                FOREIGN KEY(b_id) REFERENCES book(b_id),
	                PRIMARY KEY(b_id, r_time)
                );");
            create_table(@"
                CREATE TABLE IF NOT EXISTS supplier(
	                s_id integer PRIMARY KEY AUTOINCREMENT,
	                s_name VARCHAR(20)
                );");
            create_table(@" CREATE TABLE IF NOT EXISTS supplier_stock(
	                s_id integer  ,
	                b_id integer  ,
	                price REAL,
	                number integer   DEFAULT 9999999,
	                FOREIGN KEY(s_id) REFERENCES supplier(s_id),
	                FOREIGN KEY(b_id) REFERENCES book(b_id),
	                PRIMARY KEY(s_id, b_id)
                    );");
            create_table(@"
                CREATE TABLE IF NOT EXISTS purchase(
	                s_id integer  ,
	                b_id integer  ,
	                number integer  ,
	                price REAL,
	                p_time DATETIME,
	                FOREIGN KEY(b_id) REFERENCES book(b_id),
	                FOREIGN KEY(s_id) REFERENCES supplier(s_id),
	                PRIMARY KEY(s_id, b_id, p_time)
                );");
            // insertData();
        }
        private static void create_table(string sql)
        {
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }
        private static void insertData()
        {
            for (int i = 0; i < 100; ++i)
            {
                string sql = "insert into book(b_name, b_author) values(?,?) ";
                using(var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, "A" + i);
                    statement.Bind(2, "B" + i);
                    statement.Step();
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                string sql = "INSERT INTO supplier(s_name) VALUES(?)";
                using(var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, "supplier" + i);
                    statement.Step();
                }
            }

            for (int i = 0; i < 100; ++i)
            {
                string sql = "INSERT INTO supplier_stock(s_id, b_id, price) VALUES (?, ?, ?)";
                using(var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, i % 10 + 1);
                    statement.Bind(2, i + 1);
                    statement.Bind(3, new Random().Next(10, 50));
                    statement.Step();
                }
            }
            
        }
    }
   
}
