using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.Entity.Entity;
using TestCMS.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace TestCMS.Business.Concrete
{
    public class CartService : ICartService
    {
        /// <summary>
        /// 建構子DI
        /// </summary>
        private readonly ICartRepo _repo;
        public CartService(IServiceProvider provider)
        {
            _repo = provider.GetRequiredService<ICartRepo>();
        }

        public bool CartAdd(CartTable cart)
        {
            bool isOK = true;
            try
            {
                //若存在修改+1,否則新增
                var data = _repo.Get(cart.Id);
                if (data != null)
                {
                    _repo.Update(cart);
                }
                else
                {
                    _repo.Create(cart);
                }
            }
            catch(Exception ex)
            {
                isOK = false;
                throw ex;
            }
            
            return isOK;
        }

        public Task Delete(int id)
        {
            return _repo.Delete(id);
        }

        public Task<IList<CartTable>> Get()
        {
            return _repo.Get();
        }

        public Task Update(CartTable cart)
        {
            throw new NotImplementedException();
        }
    }
}
