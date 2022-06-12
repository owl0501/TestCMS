using System;
using System.Collections.Generic;
using System.Text;

namespace TestCMS.Entity.DTO
{
    public class ShippingDTO
    {
        public IList<int> CartIdList { get; set; } 
        public int CategoryId { get; set; }
    }
}
