using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    class Sale
    {
        public int BId { get; set; }
        public int Number { set; get; }
        public DateTimeOffset Time { get; set; }
    }
}
