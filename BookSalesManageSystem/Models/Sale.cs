using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    //销售记录
    class Sale
    {
        public Book Book { get; set; }
        public int Number { set; get; }
        public DateTimeOffset Time { get; set; }
        public float TotalPrice { get; set; }
    }
}
