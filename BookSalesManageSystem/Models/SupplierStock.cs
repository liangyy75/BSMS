using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    public class SupplierStock
    {
        public Supplier Supplier { get; set; }
        public Book Book { get; set; }
        public int Number { get; set; }
        public float Price { get; set; }
    }
}
