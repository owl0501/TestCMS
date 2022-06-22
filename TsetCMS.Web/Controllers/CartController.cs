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
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public CartController(IServiceProvider provider)
        {
            _categoryService = provider.GetService<ICategoryService>();
            _cartService = provider.GetService<ICartService>();
            _productService = provider.GetService<IProductService>();
        }

        [HttpGet]
        public IActionResult CartQueryResult()
        {
            var items = _cartService.Get();
            _productService.Get().ToList();
            _categoryService.Get().ToList();
            ViewData["CartAmount"] = _cartService.CartAmoutToViewData();
            return View(items);
        }

        public IActionResult CartItemAdd(int productId)
        {
            int cartId = _cartService.CreateCartItem(productId);
            return RedirectToAction("ProductQueryResult","Product");
        }

        public IActionResult CartItemDelete(int id)
        {
            _cartService.Delete(id);
            return RedirectToAction("CartQueryResult");
        }


        public IActionResult CartItemAmountUpdate(int id,string mode)
        {
            int cartId = _cartService.UpdateItemAmount(id, mode);
            return RedirectToAction("CartQueryResult");
        }

    }
}
