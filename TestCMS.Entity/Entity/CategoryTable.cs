using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name ="產品類型")]
        [Required(ErrorMessage ="請填寫產品類型")]
        public string Name { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
