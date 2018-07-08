using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    //进货表
    class Purchase
    {
        public Book Book { get; set; }
        public Supplier Supplier { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public DateTimeOffset Time{get; set;}
    }
}
