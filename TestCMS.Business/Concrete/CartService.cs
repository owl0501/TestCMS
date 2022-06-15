﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCMS.Business.Abstract;
using TestCMS.Entity.Entity;
using TestCMS.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TestCMS.Entity.DTO;
using Microsoft.Extensions.Logging;

namespace TestCMS.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly IGeneralRepo<CartTable> _cartRepo;
        private readonly ILogger<ICartService> _logger;

        public CartService(ILogger<ICartService> logger, IServiceProvider provider)
        {
            _cartRepo = provider.GetRequiredService<IGeneralRepo<CartTable>>();
            _logger = logger;
        }


        public IEnumerable<CartTable> Get()
        {
            return _cartRepo.Filter();
        }
        public int CreateCartItem(int productId)
        {
            int cartId = -1;
            try
            {
                //若存在修改+1,否則新增
                var cartItem = _cartRepo.Filter(d => d.ProductId == productId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Amount++;
                    cartId = (int)_cartRepo.Update(cartItem);
                }
                else
                {
                    CartTable newItem = new CartTable
                    {
                        ProductId = productId,
                        Amount = 1,
                    };
                    cartId = (int)_cartRepo.Create(newItem);
                }
                _logger.LogInformation($"新增成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return cartId;
        }

        public int UpdateItemAmount(int cartId, string mode)
        {
            var item = _cartRepo.Filter(d => d.Id == cartId).FirstOrDefault();
            if (item != null)
            {
                if (mode == "increase")
                {
                    item.Amount++;
                }
                else if (item.Amount > 1)
                {
                    item.Amount--;
                }
            }
            return (int)_cartRepo.Update(item); ;
        }

        public void Delete(int cartId)
        {
            var item = _cartRepo.Filter(d => d.Id == cartId).FirstOrDefault();
            _cartRepo.Delete(item);
        }

        public int CartAmoutToViewData()
        {
            int totalAmout = 0;
            var items = _cartRepo.Filter(d => d.ShipCode == "no");

            foreach (var item in items)
            {
                totalAmout += item.Amount;
            }
            return totalAmout;
        }

        public string UpdateAllByShipCode(ShippingDTO dto, string shipCode)
        {
            try
            {
                foreach (var cartId in dto.CartIdList)
                {
                    CartTable item = _cartRepo.Filter(d => d.Id == cartId).FirstOrDefault();
                    if (item != null)
                    {
                        item.ShipCode = shipCode;
                        _cartRepo.Update(item);
                    }
                }
                return "200";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return "415";
            }
        }

        public string DeleteItemsByShipCode(string shipCode)
        {
            try
            {
                IEnumerable<CartTable> items = _cartRepo.Filter(d => d.ShipCode == shipCode);
                _cartRepo.DeleteAll(items);
                return "200";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return "415";
            }

        }
    }
}
