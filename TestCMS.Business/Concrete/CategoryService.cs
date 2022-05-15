using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.DataAccess.Abstract;
using TestCMS.Entity.Entity;

namespace TestCMS.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly ICategoryRepo _repo;
        public CategoryService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<ICategoryRepo>();
        }
        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<CategoryTable> CreateCategory(CategoryTable category)
        {
            return _repo.Create(category);
        }
        /// <summary>
        /// 刪除類別
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Delete(int id)
        {
            return _repo.Delete(id);
        }

        /// <summary>
        /// 查詢所有類別
        /// </summary>
        /// <returns></returns>
        public Task<IList<CategoryTable>> Get()
        {
            return _repo.Get();
        }
    }
}
