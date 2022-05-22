using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Abstract
{
    public interface ICartRepo
    {
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        Task<IList<CartTable>> Get();

        Task<CartTable> Get(int Id);
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<CartTable> Create(CartTable cart);

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task Update(CartTable cart);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
