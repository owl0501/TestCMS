using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class ShippingTable
    {
        public int Id { get; set; }
        public string ShipId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual CategoryTable Category { get; set; }
    }
}
