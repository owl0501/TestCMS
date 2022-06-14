﻿using Microsoft.AspNetCore.Mvc;
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
        IEnumerable<CartTable> Get();
        /// <summary>
        /// 加入待出貨清單
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        int CreateCartItem(int productId);
        /// <summary>
        /// 刪除待出貨商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(int id);
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        int UpdateItemAmount(int cartId, string mode);
        /// <summary>
        /// 傳amount to View
        /// </summary>
        /// <returns></returns>
        int CartAmoutToViewData();
    }
}
