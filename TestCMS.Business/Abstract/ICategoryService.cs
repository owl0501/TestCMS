using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;
namespace TestCMS.Business.Abstract
{
    public interface ICategoryService
    {
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        Task<IList<CategoryTable>> Get();

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<CategoryTable> CreateCategory(CategoryTable category);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
