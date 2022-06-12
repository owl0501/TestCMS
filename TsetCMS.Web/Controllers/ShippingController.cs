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
using TestCMS.Entity.DTO;

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
        [HttpPost]
        public IActionResult ShippingAdd([FromBody]ShippingDTO dto)
        {
            string newShipId = CreateShipId();
            //變更所有的勾選的Cart item的shippId
            foreach (var cartId in dto.CartIdList)
            {
                var item = _context.CartTable.Find(cartId);
                if (item != null)
                {
                    item.ShipStatus = newShipId;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }
            //新增shipping item
            ShippingTable ship = new ShippingTable
            {
                ShipId = DateTime.Now.ToString("yyyyMMddHHmm")+dto.CategoryId+newShipId,
                CategoryId = dto.CategoryId,
                CreateTime = DateTime.Now
            };
            _context.ShippingTable.Add(ship);
            _context.SaveChanges();

            return Ok($"Category is {dto.CategoryId}");
        }
        //顯示出貨清單
        [HttpGet]
        public IActionResult ShippingListResult()
        {
            var query = _context.ShippingTable.ToList();
            return View(query);
        }

        //刪除商品
        public IActionResult Complete(string shipId)
        {
            //刪除CartTable
            string shipStatus = shipId.Substring(shipId.Length - 2, 2);
            var items = _context.CartTable.Where(d => d.ShipStatus == shipStatus);
            _context.CartTable.RemoveRange(items);
            _context.SaveChanges();
            //刪除ShippingTable
            var item = _context.ShippingTable.FirstOrDefault(d => d.ShipId == shipId);
            _context.ShippingTable.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("ShippingListResult");
        }

        public string CreateShipId()
        {
            string shipId = "";
            Random r = new Random();
            string rn = String.Format("{0:00}", r.Next(0, 100).ToString());
            var shipIdList = _context.ShippingTable.ToList();
                             
            while(shipIdList.Where(d=>d.ShipId==rn).Any())
            {
                rn = String.Format("{0:00}", r.Next(0, 100).ToString());
            }
            shipId = rn;
            return shipId;
        }
    }
}
