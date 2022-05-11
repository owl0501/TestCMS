using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.Entity.Entity;
using TestCMS.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TestCMS.Business.Concrete
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly IProductRepo _repo;
        public ProductService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<IProductRepo>();
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

        public Task<IEnumerable<ProductTable>> Get()
        {
            return _repo.Get();
        }

        public Task<IEnumerable<ProductTable>> Get(int categoryId)
        {
            return _repo.Get(categoryId);
        }

        public Task Update(ProductTable product)
        {
            return _repo.Update(product);
        }
    }
}
