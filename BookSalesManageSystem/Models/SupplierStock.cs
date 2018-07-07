using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    class SupplierStock
    {
        public Supplier Supplier { get; set; }
        public Book book { get; set; }
        public int Number { get; set; }
        public float Price { get; set; }
    }
}
