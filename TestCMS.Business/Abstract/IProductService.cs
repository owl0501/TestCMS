using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;
using TestCMS.Entity.VM;

namespace TestCMS.Business.Abstract
{
    public interface IProductService
    {
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductTable> Get();

        /// <summary>
        /// 以類別查詢
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<ProductTable> GetByCategoryId(int categoryId);

        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int CreateProduct(ProductTable product, IFormFile file, string rootPath);


        /// <summary>
        /// 取得編輯資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object GetEditData(int? id);
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        IActionResult Update(ProductTable product);

        /// <summary>
        /// 檢查產品是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ProductExists(int id);

    }
}
