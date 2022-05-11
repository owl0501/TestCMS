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

namespace TestCMS.Business.Concrete
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly IProductRepo _repo;
        private readonly ICategoryService _categoryService;
        public ProductService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<IProductRepo>();
            _categoryService = provider.GetRequiredService<ICategoryService>();
        }

        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <param name="altPath"></param>
        /// <returns></returns>
        public Task<ProductTable> CreateProduct(ProductTable product, IFormFile file, string altPath)
        {
            using (var stream = new FileStream(altPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            //設定資料
            //name
            //CategoryId
            //image path
            product.Image = altPath;
            //intro
            //releaseDatetime(YYMMDDhhmm)
            product.ReleaseDatetime = DateTime.Now;
            //SupplyStatus

            return _repo.Create(product);
        }

        public Task Delete(int id)
        {
            return _repo.Delete(id);
        }
        /// <summary>
        /// 查詢所有顯示資料
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ProductTable>> Get()
        {
            return await _repo.Get();
        }
        /// <summary>
        /// 以類別查詢
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Task<IList<ProductTable>> Get(string category)
        {
            return _repo.Get(category);
        }

        public Task Update(ProductTable product)
        {
            return _repo.Update(product);
        }
    }
}
