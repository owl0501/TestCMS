using System;
using System.Collections.Generic;
using System.Text;

namespace TestCMS.DataAccess.Abstract
{
    public interface IGeneralRepo<TEntity>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        object Create(TEntity entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        object Update(TEntity entity);

        /// <summary>
        /// 刪除 - 單項
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 刪除 - 多項
        /// </summary>
        /// <param name="entities"></param>
        void DeleteAll(IEnumerable<TEntity> entities);
        /// <summary>
        /// 查詢 - 全部
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Filter();
        /// <summary>
        /// 查詢 - 限條件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
    }
}
