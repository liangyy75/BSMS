﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSalesManageSystem.Models
{
    class Return
    {
        public Book book;
        public int Number { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
