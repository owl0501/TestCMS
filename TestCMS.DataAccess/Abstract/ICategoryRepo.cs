﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Abstract
{
    public interface ICategoryRepo
    {
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        Task<IList<CategoryTable>> GetAsync();

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<CategoryTable> Create(CategoryTable category);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
