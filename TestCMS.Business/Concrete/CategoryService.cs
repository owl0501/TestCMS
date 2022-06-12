using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IGeneralRepo<CategoryTable> _repo;
        public CategoryService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<IGeneralRepo<CategoryTable>>();
        }

        public bool CategoryExists(string name)
        {
            return _repo.Filter().Any(d => d.Name == name);
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string CreateCategory(CategoryTable category)
        {
            string msg;
            if (CategoryExists(category.Name))
            {
                _repo.Create(category);
                msg = "成功";
            }
            else
            {
                msg = "已存在";
            }
            return msg;
        }
        /// <summary>
        /// 查詢所有類別
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryTable> Get()
        {
            return _repo.Filter();
        }
    }
}
