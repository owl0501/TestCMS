using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCMS.DataAccess.Abstract;
using TestCMS.Entity.Entity;

namespace TestCMS.DataAccess.Concrete
{
    public class CartRepo : ICartRepo
    {
        /// <summary>
        /// DI
        /// </summary>
        private readonly CMSDBContext _context;
        public CartRepo(IServiceProvider provider, CMSDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<CartTable> Create(CartTable cart)
        {
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var item = await _context.CartTable.FindAsync(id);
            _context.CartTable.Remove(item);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 查詢資料
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CartTable>> Get()
        {
            return await _context.CartTable.ToListAsync();
        }
        /// <summary>
        /// 條件查詢
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CartTable> Get(int Id)
        {
            return await _context.CartTable.FindAsync(Id);
        }
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task Update(CartTable cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
