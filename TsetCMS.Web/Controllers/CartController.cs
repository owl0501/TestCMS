using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.Entity.Entity;
using Microsoft.Extensions.DependencyInjection;


namespace TsetCMS.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly CMSDBContext _context;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        public int CartAmount = 0;
        public CartController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {

            _context = context;
            _logger = logger;
            _categoryService = provider.GetService<ICategoryService>();
        }

        [HttpGet]
        public async Task<IActionResult> CartIndex()
        {
            var result = from data in _context.CartTable.Include(p => p.Product) select data;
            var categoryQuery = await _categoryService.Get();

            CartAmoutToViewData();
            return View(result);
        }

        public async Task<IActionResult> CartAdd(int pId)
        {
            var result = _context.CartTable.FirstOrDefault(x => x.ProductId == pId && x.ShipStatus=="no");
            if (result != null)
            {
                //update
                result.Amount++;
                _context.CartTable.Update(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                //add
                CartTable c = new CartTable
                {
                    ProductId = pId,
                    Amount = 1,
                };
                _context.CartTable.Add(c);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index","Product");
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var item = await _context.CartTable.FindAsync(id);
            _context.CartTable.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CartIndex");
        }


        public async Task<IActionResult> UpdateAmount(int pId,string mode)
        {
            var result = _context.CartTable.FirstOrDefault(x => x.ProductId == pId);
            if (result != null)
            {
                if (mode == "increase")
                {
                    result.Amount++;
                }
                else if (result.Amount > 1)
                {
                    result.Amount--;
                }
                
                _context.CartTable.Update(result);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CartIndex");
        }
        public IActionResult Shipping()
        {
            return View();
        }

        /// <summary>
        /// 傳amount to View
        /// </summary>
        public void CartAmoutToViewData()
        {
            var cartQuery = _context.CartTable.ToList();
            var cartItem = cartQuery.Where(d => d.ShipStatus == "no");
            foreach (var item in cartItem)
            {
                CartAmount += item.Amount;
            }
            ViewData["CartAmount"] = CartAmount;
        }


    }
}
