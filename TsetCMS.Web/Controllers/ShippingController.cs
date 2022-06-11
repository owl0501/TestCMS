using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class ShippingController : Controller
    {

        private readonly CMSDBContext _context;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        public int CartAmount = 0;
        public ShippingController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {

            _context = context;
            _logger = logger;
            _categoryService = provider.GetService<ICategoryService>();
        }

        //新增出貨商品
        public IActionResult ShippingAdd(IList<int> cartIdList,int CategoryId)
        {
            string newShipId = CreateShipId();
            ////變更所有的勾選的Cart item的shippId
            foreach (var cartId in cartIdList)
            {
                var item = _context.CartTable.Find(cartId);
                if (item != null)
                {
                    //item.shipId = newShipId;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }
            //新增shipping item
            ShippingTable ship = new ShippingTable
            {
                ShipId = newShipId,
                ProductId = CategoryId,
                CreateDate = DateTime.Now
            };
            _context.ShippingTable.Add(ship);
            _context.SaveChanges();
            return RedirectToAction("CartIndex", "Cart");
        }
        //顯示出貨清單
        public IActionResult ShippingListResult()
        {
            var query = _context.ShippingTable.ToList();
            return View(query);
        }

        //刪除商品
        public async Task<IActionResult> Complete(int id)
        {
            var item = await _context.ShippingTable.FindAsync(id);
            _context.ShippingTable.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CartIndex","Cart");
        }

        public string CreateShipId()
        {
            string shipId = "";
            Random r = new Random();
            string rn = String.Format("{0:00}", r.Next(0, 100).ToString());
            var shipIdList = _context.ShippingTable.ToList();
                             
            while(shipIdList.Where(d=>d.ShipId==rn)!=null)
            {
                rn = String.Format("{0:00}", r.Next(0, 100).ToString());
            }
            shipId = rn;
            return shipId;
        }
    }
}
