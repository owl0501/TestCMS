using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;

namespace TsetCMS.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly CMSDBContext _context;
        private readonly ILogger<ProductController> _logger;
        public int CartAmount = 0;
        public CartController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult CartIndex()
        {
            var result = from data in _context.CartTable.Include(p => p.Product) select data;

            CartAmoutToViewData();
            return View(result);
        }

        public async Task<IActionResult> CartAdd(int pId)
        {
            //string a = pId;
            var result = _context.CartTable.FirstOrDefault(x => x.Product_id == pId);
            if (result != null)
            {
                //update
                result.Amount++;
                _context.CartTable.Update(result);
                _context.SaveChanges();
            }
            else
            {
                //add
                CartTable c = new CartTable
                {
                    Product_id = pId,
                    Amount = 1,
                };
                _context.CartTable.Add(c);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.CartTable.FindAsync(id);
            _context.CartTable.Remove(item);
            await _context.SaveChangesAsync();
            return View();
        }

        private void CartAmoutToViewData()
        {
            var cartQuery = _context.CartTable.ToList();
            foreach (var item in cartQuery)
            {
                CartAmount += item.Amount;
            }
            ViewData["CartAmount"] = CartAmount;
        }
    }
}
