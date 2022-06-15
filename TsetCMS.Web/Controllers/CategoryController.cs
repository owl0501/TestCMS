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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;
        public CategoryController(ILogger<CategoryController> logger, IServiceProvider provider)
        {
            _categoryService = provider.GetService<ICategoryService>();
            _cartService = provider.GetService<ICartService>();
        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            ViewData["CartAmount"] = _cartService.CartAmoutToViewData();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoryAdd(CategoryTable category)
        {
            string msg = "失敗";
            if (ModelState.IsValid)
            {
                if (category.Name != null)
                {
                    msg = _categoryService.CreateCategory(category);
                }
            }
            ViewBag.isOK = msg;
            return View();
        }
    }
}
