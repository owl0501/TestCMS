using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class ShippingTable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShipId { get; set; }

        public virtual ProductTable Product { get; set; }
    }
}
