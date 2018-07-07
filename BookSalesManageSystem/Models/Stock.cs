using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    class Stock
    {
        public Book book;
        public int Number { get; set; }
        public float OfferPrice { get; set; }
        public float SalePrice { get; set; }
    }
}
