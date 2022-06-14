using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.Entity.Entity;
using TestCMS.Entity.VM;
using TestCMS.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using TestCMS.Helper;
using Microsoft.AspNetCore.Mvc;

namespace TestCMS.Business.Concrete
{
    public class ProductService : ControllerBase, IProductService
    {

        private readonly IGeneralRepo<ProductTable> _productRepo;
        private readonly ICategoryService _categoryService;
        public ProductService(IServiceProvider provider)
        {
            _productRepo = provider.GetRequiredService<IGeneralRepo<ProductTable>>();
            _categoryService = provider.GetRequiredService<ICategoryService>();
        }

        public int CreateProduct(ProductTable product, IFormFile image, string rootPath)
        {
            //設定資料
            product.Image = SaveImage(rootPath, image);
            product.ReleaseDatetime = DateTime.Now;
            //SupplyStatus
            return (int)_productRepo.Create(product);
        }

        public IEnumerable<ProductTable> Get()
        {
            _categoryService.Get().ToList();
            return _productRepo.Filter();
        }

        public IEnumerable<ProductTable> GetByCategoryId(int categoryId)
        {
            return _productRepo.Filter(d => d.CategoryId == categoryId);
        }

        public object GetEditData(int? id)
        {
            _categoryService.Get().ToList();
            var product = _productRepo.Filter(d => d.Id == id).FirstOrDefault();
            return product;
        }

        public IActionResult Update(ProductTable product, IFormFile image, string rootPath)
        {
            if (image != null)
            {
                product.Image = SaveImage(rootPath, image);
            }
            _productRepo.Update(product);
            return Ok();
        }

        public bool ProductExists(int id)
        {
            return _productRepo.Filter().Any(d => d.Id == id);
        }

        public string SaveImage(string rootPath, IFormFile image)
        {
            //另存圖片路徑
            string saveFolder = @"\UploadFolder\";
            string altPath = rootPath + saveFolder;
            string fullPath = altPath + image.FileName;
            if (!Directory.Exists(altPath))
            {
                Directory.CreateDirectory(altPath);
            }
            //檢查是否存在
            if (!System.IO.File.Exists(fullPath))
            {
                //另存圖片
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }

            return saveFolder + image.FileName;
        }

    }
}
