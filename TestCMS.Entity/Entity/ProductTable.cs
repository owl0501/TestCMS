using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class ProductTable
    {
        public ProductTable()
        {
            CartTables = new HashSet<CartTable>();
            ShippingTables = new HashSet<ShippingTable>();
        }

        public int Id { get; set; }
        [Display(Name="產品名稱")]
        [Required(ErrorMessage ="產品名稱為必填欄位")]
        public string Name { get; set; }
        [Display(Name = "產品類型")]
        [Required(ErrorMessage = "產品類型為必填欄位")]
        public int CategoryId { get; set; }
        [Display(Name = "產品圖片")]
        [Required(ErrorMessage = "產品圖片為必填欄位")]
        public string Image { get; set; }
        [Display(Name = "產品說明")]
        public string Intro { get; set; }
        public DateTime ReleaseDatetime { get; set; }
        public string SupplyStatus { get; set; }

        public virtual CategoryTable Category { get; set; }
        public virtual ICollection<CartTable> CartTables { get; set; }
        public virtual ICollection<ShippingTable> ShippingTables { get; set; }
    }
}
