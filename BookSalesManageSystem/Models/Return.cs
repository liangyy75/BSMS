using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    public class Return
    {
        public Book Book { get; set; }
        public int Number { get; set; }
        public DateTimeOffset Time { get; set; }
        public float TotalPrice { get; set; }
    }
}
