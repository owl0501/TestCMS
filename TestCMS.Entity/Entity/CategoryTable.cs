using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class CategoryTable
    {
        public CategoryTable()
        {
            ProductTables = new HashSet<ProductTable>();
            ShippingTables = new HashSet<ShippingTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
        public virtual ICollection<ShippingTable> ShippingTables { get; set; }
    }
}
