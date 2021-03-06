﻿using BookSalesManageSystem.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Utils
{
    public class SupplierStockUtil
    {
        // 查询
        public static List<SupplierStock> QuerySupplierStock(int id)
        {
            List<SupplierStock> supplierStocks = new List<SupplierStock>();
            using (var statement = SqlUtil.conn.Prepare("SELECT S.s_id, S.s_name, B.b_id, B.b_name, B.b_author, SS.number, SS.price " +
                "FROM supplier_stock SS, book B, supplier S " +
                "WHERE SS.b_id = ? and SS.b_id = B.b_id and SS.s_id = S.s_id"))
            {
                statement.Bind(1, id);
                while (SQLiteResult.ROW == statement.Step())
                {
                    Supplier supplier = new Supplier { SId = int.Parse(statement[0].ToString()), SName = (string)statement[1] };
                    Book book = new Book { BId = int.Parse(statement[2].ToString()), BName = (string)statement[3], BAuthor = (string)statement[4] };
                    SupplierStock supplierStock = new SupplierStock { Supplier = supplier, Book = book, Number = int.Parse(statement[5].ToString()), Price = float.Parse(statement[6].ToString()) };
                    supplierStocks.Add(supplierStock);
                }
            }
            return supplierStocks;
        }

        // 更改
        public static void UpdateSupplierStock(int s_id, int b_id, int number)
        {
            using (var statement = SqlUtil.conn.Prepare("UPDATE supplier_stock SET number = number + ? WHERE s_id = ? and b_id = ?"))
            {
                statement.Bind(1, number);
                statement.Bind(2, s_id);
                statement.Bind(3, b_id);
                statement.Step();
            }
        }
    }
}
