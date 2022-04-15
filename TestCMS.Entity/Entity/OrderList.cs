using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class OrderList
    {
        public string Id { get; set; }
        public int? ProductId { get; set; }
        public int? Stock { get; set; }

        public virtual Product Product { get; set; }
    }
}
