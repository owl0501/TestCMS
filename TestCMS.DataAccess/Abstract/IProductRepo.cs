using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Abstract
{
    public interface IProductRepo
    {
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        Task<IList<ProductTable>> Get();

        /// <summary>
        /// 以類別查詢
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<IList<ProductTable>> Get(string categoryId);

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductTable> Create(ProductTable product);

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task Update(ProductTable product);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
