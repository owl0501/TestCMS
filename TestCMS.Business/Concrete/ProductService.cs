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
    public class ProductService : ControllerBase,IProductService
    {

        private readonly IGeneralRepo<ProductTable> _productRepo;
        public ProductService(IServiceProvider provider)
        {
            _productRepo = provider.GetRequiredService<IGeneralRepo<ProductTable>>();
        }

        public int CreateProduct(ProductTable product, IFormFile file, string rootPath)
        {
            //另存圖片路徑
            string saveFolder = @"\UploadFolder\";
            string altPath = rootPath + saveFolder;

            if (!Directory.Exists(altPath))
            {
                DirectoryInfo imgFolder = Directory.CreateDirectory(altPath);
            }
            //另存圖片
            using (var stream = new FileStream(altPath + file.FileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            //設定資料
            product.Image = saveFolder + file.FileName;
            product.ReleaseDatetime = DateTime.Now;
            //SupplyStatus
            return (int)_productRepo.Create(product);
        }

        public IEnumerable<ProductTable> Get()
        {

            return _productRepo.Filter();
        }

        public IEnumerable<ProductTable> GetByCategoryId(int categoryId)
        {
            return _productRepo.Filter(d => d.CategoryId == categoryId);
        }

        public object GetEditData(int? id)
        {
            var product = _productRepo.Filter(d=>d.Id==id).FirstOrDefault();
            return product;
        }

        public IActionResult Update(ProductTable product)
        {
            if (!product.Image.Contains("UploadFolder"))
            {
                product.Image = ImageHelper.SaveImagePath(product.Image);
            }
            _productRepo.Update(product);
            return Ok();
        }

        public bool ProductExists(int id)
        {
            return _productRepo.Filter().Any(d => d.Id == id);
        }
    }
}
