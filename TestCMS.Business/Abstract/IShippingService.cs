using System;
using System.Collections.Generic;
using System.Text;
using TestCMS.Entity.DTO;
using TestCMS.Entity.Entity;

namespace TestCMS.Business.Abstract
{
    public interface IShippingService
    {
        /// <summary>
        /// 新增出貨清單
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int CreateItem(ShippingDTO dto);
        /// <summary>
        /// 取得所有清單
        /// </summary>
        /// <returns></returns>
        IEnumerable<ShippingTable> Get();
        /// <summary>
        /// 刪除清單
        /// </summary>
        /// <param name="code"></param>
        void Delete(string code);
        /// <summary>
        /// 建立出貨代碼
        /// </summary>
        /// <returns></returns>
        string CreateShipCode();


    }
}
