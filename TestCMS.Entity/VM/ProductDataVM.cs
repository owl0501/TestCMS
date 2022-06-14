using System;
using System.Collections.Generic;
using System.Text;
using TestCMS.Entity.Entity;

namespace TestCMS.Entity.VM
{
    public class ProductDataVM
    {
        public ProductTable Product; 
        public bool IsNew { get; set; }
    }
}
