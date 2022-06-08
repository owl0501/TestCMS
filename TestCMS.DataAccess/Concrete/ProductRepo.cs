using System;
using System.Collections.Generic;
using System.Text;
using TestCMS.Entity.Entity;
using TestCMS.DataAccess.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestCMS.DataAccess.Concrete
{
    public class ProductRepo:IProductRepo
    {
        public readonly CMSDBContext _context;
        public ProductRepo(CMSDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ProductTable> Create(ProductTable product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync(); 
            return product;
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Delete(int id)
        {
            var product = await _context.ProductTable.FindAsync(id);
            _context.ProductTable.Remove(product);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ProductTable>> Get()
        {
            var result = from data in _context.ProductTable.Include(p => p.Category) select data;
            return await result.ToListAsync();
        }
        /// <summary>
        /// 以類別查詢
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<IList<ProductTable>> Get(string category)
        {
            var result = from data in _context.ProductTable.Include(p => p.Category) select data;
            result = result.Where(data => data.Category.Name == category);
            return await result.ToListAsync();
        }
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task Update(ProductTable product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
