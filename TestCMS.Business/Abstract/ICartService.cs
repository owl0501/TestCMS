using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;

namespace TestCMS.Business.Abstract
{
    public interface ICartService
    {
        /// <summary>
        /// 查詢所有項目
        /// </summary>
        /// <returns></returns>
        Task<IList<CartTable>> Get();
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool CartAdd(CartTable cart);
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task Update(CartTable cart);
    }
}
