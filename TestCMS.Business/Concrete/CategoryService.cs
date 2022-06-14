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
        private readonly IGeneralRepo<CategoryTable> _repo;
        public CategoryService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<IGeneralRepo<CategoryTable>>();
        }
        public string CreateCategory(CategoryTable category)
        {
            string msg;
            if (!CategoryExists(category.Name))
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
        public IEnumerable<CategoryTable> Get()
        {
            return _repo.Filter();
        }

        public bool CategoryExists(string name)
        {
            return _repo.Filter().Any(d => d.Name == name);
        }
    }
}
