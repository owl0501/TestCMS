using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestCMS.Entity.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace TsetCMS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CMSDBContext _context;
        private readonly ILogger<ProductController> _logger;
        private readonly string _folder;
        public ProductController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;

            // 預設上傳目錄下(wwwroot\UploadFolder)
            _folder = $@"{env.WebRootPath}\UploadFolder";

        }

        public async Task<IActionResult> Index()
        {
            var query = from c in _context.CategoryTable
                        select c;
            HomeVM homeVM = new HomeVM
            {
                Categories = await query.ToListAsync()
            };
            

            return View(homeVM);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            //傳Categories model給create view
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTable product, IFormFile myimg)
        {
            string altPath;
            if (!Directory.Exists(_folder))
            {
                DirectoryInfo di = Directory.CreateDirectory(_folder);
            }

            //IFormFile name對應input type=file的name屬性)
            if (ModelState.IsValid)
            {
                
                if (myimg != null)
                {
                    //另存圖片
                    altPath = $@"{_folder}\{myimg.FileName}";
                    using (var stream = new FileStream(altPath, FileMode.Create))
                    {
                        await myimg.CopyToAsync(stream);
                    }
                    product.Image = altPath;
                }

                //設定資料
                //name

                //CategoryId

                //image path

                //intro

                //releaseDatetime(YYMMDDhhmm)
                product.ReleaseDatetime = DateTime.Now;
                //SupplyStatus

                //對資料庫新增資料
                _context.Add(product);
                //儲存資料
                await _context.SaveChangesAsync();
                //重新路由
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categories"] = new SelectList(_context.Set<CategoryTable>(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public IActionResult Pending()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryTable category)
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
    }
}
