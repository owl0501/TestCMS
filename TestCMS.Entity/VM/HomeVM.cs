using System;
using System.Collections.Generic;
using System.Text;
using TestCMS.Entity.Entity;
namespace TestCMS.Entity.VM
{
    public class HomeVM
    {
        //產品列表
        public List<ProductTable> Products { get; set; }
        //類別集合
        public List<CategoryTable> Categories { get; set; }
        //待出貨清單數量
        
        //類別查詢
        public string Category_query;
    }
}
