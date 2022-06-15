using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class ShippingTable
    {
        public int Id { get; set; }
        [Display(Name = "出貨編號")]
        public string ShipId { get; set; }
        [Display(Name = "產品類型")]
        public int CategoryId { get; set; }
        [Display(Name = "建立日期")]
        public DateTime CreateTime { get; set; }
        public string ShipCode { get; set; }

        public virtual CategoryTable Category { get; set; }
    }
}
