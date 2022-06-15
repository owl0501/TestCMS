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
        private readonly IShippingService _shippingService;
        private readonly ICartService _cartService;
        public ShippingController(IWebHostEnvironment env, IServiceProvider provider)
        {
            _shippingService = provider.GetService<IShippingService>();
            _cartService = provider.GetService<ICartService>();
        }

        //新增出貨商品
        [HttpPost]
        public IActionResult ShippingItemCreate([FromBody]ShippingDTO dto)
        {
            _shippingService.CreateItem(dto);
            return Ok($"Category is {dto.CategoryId}");
        }
        //顯示出貨清單
        [HttpGet]
        public IActionResult ShippingQueryResult()
        {
            var query = _shippingService.Get();
            ViewData["CartAmount"] = _cartService.CartAmoutToViewData();
            return View(query);
        }

        //刪除商品
        public IActionResult ShippingItemComplete(string shipCode)
        {
            _shippingService.Delete(shipCode);
            return RedirectToAction("ShippingQueryResult");
        }
    }
}
