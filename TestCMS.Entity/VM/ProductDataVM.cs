using System;
using System.Collections.Generic;
using System.Text;

namespace TestCMS.Entity.VM
{
    public class ProductDataVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string  Name { get; set; }
        public string Category { get; set; }
        public string  Intro { get; set; }
        public string SupplyState { get; set; }
        public bool IsNew { get; set; }
    }
}
