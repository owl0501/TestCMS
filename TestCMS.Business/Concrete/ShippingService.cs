using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCMS.Business.Abstract;
using TestCMS.DataAccess.Abstract;
using TestCMS.Entity.DTO;
using TestCMS.Entity.Entity;

namespace TestCMS.Business.Concrete
{
    public class ShippingService : IShippingService
    {
        private readonly IGeneralRepo<ShippingTable> _shippingRepo;
        private readonly ICartService _cartService;

        public ShippingService(IServiceProvider provider)
        {
            _shippingRepo = provider.GetRequiredService<IGeneralRepo<ShippingTable>>();
            _cartService = provider.GetRequiredService<ICartService>();
        }
        public int CreateItem(ShippingDTO dto)
        {
            string newCode = CreateShipCode();
            //變更所有的勾選的Cart item的shippId
            string code = _cartService.UpdateAllByShipCode(dto, newCode);
            //新增shipping item
            ShippingTable shipItem = new ShippingTable
            {
                ShipId = DateTime.Now.ToString("yyyyMMddHHmm") + dto.CategoryId + newCode,
                CategoryId = dto.CategoryId,
                CreateTime = DateTime.Now,
                ShipCode = newCode
            };
            _shippingRepo.Create(shipItem);
            return shipItem.Id;
        }

        public IEnumerable<ShippingTable> Get()
        {
            return _shippingRepo.Filter();
        }

        public void Delete(string shipCode)
        {
            //刪除CartTable
            string code = _cartService.DeleteItemsByShipCode(shipCode);
            //刪除ShippingTable
            var item = _shippingRepo.Filter(d => d.ShipCode == shipCode).FirstOrDefault();
            if (item != null)
            {
                _shippingRepo.Delete(item);
            }
        }

        public string CreateShipCode()
        {
            string randomNumber = "";
            Random r = new Random();
            bool isExist = false;
            do
            {
                randomNumber = String.Format("{0:00}", r.Next(0, 100).ToString());
                isExist = _shippingRepo.Filter(d => d.ShipCode == randomNumber).Any();
            } while (isExist);

            return randomNumber;
        }
    }
}
