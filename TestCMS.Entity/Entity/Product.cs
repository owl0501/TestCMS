using System;
using System.Collections.Generic;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class Product
    {
        public Product()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
