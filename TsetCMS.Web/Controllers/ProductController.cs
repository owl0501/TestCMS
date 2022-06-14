using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using TestCMS.Entity.VM;
using TestCMS.Entity.Entity;
using TestCMS.Business.Abstract;
using TestCMS.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace TsetCMS.Web.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;
        private readonly ILogger<ProductController> _logger;
        private readonly string _wwwroot;
        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {
            _logger = logger;
            _productService = provider.GetRequiredService<IProductService>();
            _categoryService = provider.GetService<ICategoryService>();
            _cartService = provider.GetService<ICartService>();
            // 預設上傳目錄下(wwwroot)
            _wwwroot = $@"{env.WebRootPath}";
        }

        [HttpGet]
        public IActionResult ProductQueryResult(string searchStr, string currentFilter, int? pageNumber)
        {
            var productQuery = _productService.Get();

            if (searchStr != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchStr = currentFilter;
            }
            //紀錄查詢字串
            ViewData["CurrentFilter"] = searchStr;

            if (searchStr != null)
            {
                productQuery = productQuery.Where(data => data.Category.Name == searchStr).ToList();
            }
            //
            IList<ProductDataVM> pvm = new List<ProductDataVM>();
            foreach (var item in productQuery)
            {
                pvm.Add(new ProductDataVM
                {
                    Product = item,
                    IsNew = DateTime.Now.Subtract(item.ReleaseDatetime).Days <= 14,
                });
            }

            var pq = pvm.AsQueryable();
            int pageSize = 4;
            var tmp = PaginatedList<ProductDataVM>.CreatePage(pq.AsNoTracking(), pageNumber ?? 1, pageSize);

            ViewData["Categories"] = _categoryService.Get().ToList();
            ViewData["CartAmount"] = _cartService.CartAmoutToViewData();
            return View(tmp);
        }

        [HttpGet]
        public IActionResult ProductItemCreate()
        {
            ViewData["Categories"] = new SelectList(_categoryService.Get(), "Id", "Name");
            ViewData["CartAmount"] = _cartService.CartAmoutToViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductItemCreate(ProductTable product, IFormFile myimg)
        {
            if (ModelState.IsValid)
            {
                if (myimg != null)
                {
                    _productService.CreateProduct(product, myimg, _wwwroot);
                    return RedirectToAction(nameof(ProductQueryResult));
                }
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult ProductItemEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = _productService.GetEditData(id);
            if (result != null)
            {
                ////傳Categories model給create view
                ViewData["Categories"] = new SelectList(_categoryService.Get(), "Id", "Name");
                ViewData["CartAmount"] = _cartService.CartAmoutToViewData();

                return View(result);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductItemEdit(int id, ProductTable product, IFormFile myimg)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.Update(product, myimg, _wwwroot);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!_productService.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw (ex);
                    }
                }
                return RedirectToAction(nameof(ProductQueryResult));
            }
            return View(product);
        }
    }
}
