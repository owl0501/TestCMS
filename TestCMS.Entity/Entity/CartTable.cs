using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class CartTable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string ShipCode { get; set; }

        public virtual ProductTable Product { get; set; }
    }
}
