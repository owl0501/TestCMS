using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCMS.DataAccess.Abstract;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Concrete
{
    public class GeneralRepo<TEntity> : IGeneralRepo<TEntity>
        where TEntity : class
    {

        public readonly CMSDBContext _context;
        public GeneralRepo(CMSDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public object Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            object keyValue = GetPrimaryKeyValue(entity);
            return keyValue;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        public object Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            object keyValue = GetPrimaryKeyValue(entity);
            return keyValue;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// 查詢 - 全部
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Filter()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// 查詢 - 限條件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 取得主鍵值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private object GetPrimaryKeyValue(TEntity entity)
        {
            string keyName = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            object keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);
            return keyValue;
        }
    }
}
