using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCMS.Entity.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestCMS.Entity.VM;
using Microsoft.EntityFrameworkCore;

namespace TsetCMS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CMSDBContext _context;
        private readonly ILogger<HomeController> _logger;
        public ProductController(CMSDBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var query = from c in _context.Categories
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

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            string msg = "失敗";
            try
            {
                var query = from c in _context.Categories 
                            select c;
                var tmp = query.Where(s => s.Name.Contains(category.Name));
                if (tmp.Count()<=0)
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    msg = "成功";
                }
                else
                {
                    msg = "已存在";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("資料庫新增類別錯誤");
                throw (ex);
            }
            ViewBag.isOK = msg;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Pending()
        {
            return View();
        }
    }
}
