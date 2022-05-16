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
using Microsoft.Extensions.DependencyInjection;

namespace TsetCMS.Web.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly CMSDBContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        //private readonly ICartService _cartService;
        private readonly ILogger<ProductController> _logger;
        private readonly string _root;
        private readonly string _fodler;
        public ProductController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {
            _context = context;
            _logger = logger;
            _productService = provider.GetRequiredService<IProductService>();
            _categoryService = provider.GetService<ICategoryService>();
            //_cartService = provider.GetService<ICartService>();
            // 預設上傳目錄下(wwwroot\UploadFolder)
            _fodler = $@"{env.WebRootPath}";
            _root = $@"{env.ContentRootPath}";


        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchStr)
        {
            var productQuery = await _productService.Get();
            var categoryQuery = await _categoryService.Get();
            if (searchStr != null)
            {
                productQuery = await _productService.Get(searchStr);
            }
            IList<ProductDataVM> pvm = new List<ProductDataVM>();
            foreach(var item in productQuery)
            {
                pvm.Add(new ProductDataVM
                {
                    ImagePath = item.Image.Remove(0, _fodler.Length),
                    Name = item.Name,
                    Category = item.Category.Name,
                    Intro = item.Intro,
                    SupplyState = item.SupplyStatus,
                    IsNew = DateTime.Now.Subtract(item.ReleaseDatetime).Days <= 14
                });
            }
            

            IndexVM indexVM = new IndexVM
            {
                Products = pvm,
                Categories = categoryQuery.ToList(),
            };
            return View(indexVM);
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {
            //傳Categories model給create view
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name");
            return View();
        }
        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="myimg"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductAdd(ProductTable product, IFormFile myimg)
        {
            //IFormFile name對應input type=file的name屬性)

            if (!Directory.Exists(_root))
            {
                DirectoryInfo di = Directory.CreateDirectory(_root);
            }
            if (ModelState.IsValid)
            {

                if (myimg != null)
                {
                    //另存圖片
                    string altPath = $@"{_root}\UploadFolder\{myimg.FileName}";

                    await _productService.CreateProduct(product, myimg, altPath);
                    return RedirectToAction(nameof(Index));
                }


            }
            var t = _context.Set<CategoryTable>();
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public IActionResult ProductEdit(ProductTable p)
        {
            return View();
        }
        /// <summary>
        /// 新增產品類別
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        /// <summary>
        /// 新增產品類別
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryAdd(CategoryTable category)
        {
            string msg = "失敗";
            if (ModelState.IsValid)
            {
                if (category.Name != null)
                {
                    try
                    {
                        var query = from c in _context.CategoryTable
                                    select c;
                        var tmp = query.Where(s => s.Name.Contains(category.Name));
                        if (tmp.Count() <= 0)
                        {
                            _context.CategoryTable.Add(category);
                            await _context.SaveChangesAsync();
                            msg = "成功";
                        }
                        else
                        {
                            msg = "已存在";
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("資料庫新增類別錯誤");
                        throw (ex);
                    }
                }
            }
            ViewBag.isOK = msg;
            return View();
        }
        



        #region Cart
        //public IActionResult CartAdd(CartTable cartvm)
        //{
        //    var p = _context.ProductTable.Find(9);
        //    CartTable c = new CartTable
        //    {
        //        ProductId = 1,
        //        Amount = 1,
        //    };
        //    var cc = _cartService.CartAdd(c);
        //    var cartQuery = _cartService.Get();
        //    return Redirect(nameof(Index));
        //}
        #endregion



    }
}
