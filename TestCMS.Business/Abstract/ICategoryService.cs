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
        /// 查詢所有類別
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryTable> Get();


        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        string CreateCategory(CategoryTable category);

        
        /// <summary>
        /// 檢查類型是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CategoryExists(string name);
    }
}
