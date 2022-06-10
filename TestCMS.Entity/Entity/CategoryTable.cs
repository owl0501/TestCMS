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
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
