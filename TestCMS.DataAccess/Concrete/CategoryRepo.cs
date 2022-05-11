using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.DataAccess.Abstract;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Concrete
{
    public class CategoryRepo : ICategoryRepo
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly CMSDBContext _context;
        public CategoryRepo(CMSDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<CategoryTable> Create(CategoryTable category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var category = await _context.CategoryTable.FindAsync(id);
            _context.CategoryTable.Remove(category);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CategoryTable>> GetAsync()
        {
            return await _context.CategoryTable.ToListAsync();
        }
    }
}
