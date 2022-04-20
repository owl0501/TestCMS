﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class OrderList
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Amount { get; set; }
        public string OrderId { get; set; }

        public virtual Product Product { get; set; }
    }
}
