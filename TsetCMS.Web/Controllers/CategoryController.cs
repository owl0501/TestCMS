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
        private readonly ILogger<ProductController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(CMSDBContext context, ILogger<ProductController> logger, IWebHostEnvironment env, IServiceProvider provider)
        {
            _logger = logger;
            _categoryService = provider.GetService<ICategoryService>();


        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            //CartAmoutToViewData();
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
                    try
                    {
                        msg = _categoryService.CreateCategory(category);
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
