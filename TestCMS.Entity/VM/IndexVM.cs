using System;
using System.Collections.Generic;
using System.Text;
using TestCMS.Entity.Entity;
namespace TestCMS.Entity.VM
{
    public class IndexVM
    {
        /// <summary>
        /// 產品列表
        /// </summary>
        public IList<ProductDataVM> Products { get; set; }
        /// <summary>
        /// 類別集合
        /// </summary>
        public IList<CategoryTable> Categories { get; set; }        
        /// <summary>
        /// 類別查詢
        /// </summary>
        public string Category_query { get; set; }
        /// <summary>
        /// 待出貨數量
        /// </summary>
        public int PendingCount { get; set; }
    }
}
