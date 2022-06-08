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
        private readonly CMSDBContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        //private readonly ICartService _cartService;
        private readonly ILogger<ProductController> _logger;
        private readonly string _fodler;
        public int CartAmount = 0;
        public ProductController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {
            _context = context;
            _logger = logger;
            _productService = provider.GetRequiredService<IProductService>();
            _categoryService = provider.GetService<ICategoryService>();
            //_cartService = provider.GetService<ICartService>();
            // 預設上傳目錄下(wwwroot\UploadFolder)
            _fodler = $@"{env.WebRootPath}";

            
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchStr, string currentFilter,int? pageNumber)
        {
            var productQuery = await _productService.Get();
            var categoryQuery = await _categoryService.Get();

            CartAmoutToViewData();
            if (searchStr != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchStr = currentFilter;
            }
            //
            ViewData["CurrentFilter"] = searchStr;
            //
            if (searchStr != null)
            {
                productQuery = productQuery.Where(data => data.Category.Name == searchStr).ToList();
            }
            //
            IList<ProductDataVM> pvm = new List<ProductDataVM>();
            foreach(var item in productQuery)
            {
                pvm.Add(new ProductDataVM
                {
                    Product=item,
                    IsNew = DateTime.Now.Subtract(item.ReleaseDatetime).Days <= 14,
                });
            }

            var pq = pvm.AsQueryable();
            int pageSize = 4;
            var tmp = PaginatedList<ProductDataVM>.CreateAsync(pq.AsNoTracking(), pageNumber ?? 1, pageSize);
            
            
            
            ViewData["Categories"] = categoryQuery.ToList();
            return View(tmp);
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {
            CartAmoutToViewData();
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
            if (ModelState.IsValid)
            {
                if (myimg != null)
                {
                    await _productService.CreateProduct(product, myimg, _fodler);
                    return RedirectToAction(nameof(Index));
                }
            }
            var t = _context.Set<CategoryTable>();
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int? id)
        {
            CartAmoutToViewData();
            if (id == null)
            {
                return NotFound();
            }

            var p = await _context.ProductTable.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            //傳Categories model給create view
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name");
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(int id, ProductTable product,bool isProvide)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!product.Image.Contains("UploadFolder")){
                        product.Image = ImageHelper.SaveImagePath(product.Image);
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw(ex);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
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
        private bool ProductExists(int id)
        {
            return _context.ProductTable.Any(e => e.Id == id);
        }
        /// <summary>
        /// 新增產品類別
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            CartAmoutToViewData();
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

        
        #endregion

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.CartTable.FindAsync(id);
            _context.CartTable.Remove(item);
            await _context.SaveChangesAsync();
            return View();
        }



    }
}
