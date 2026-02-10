using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BaketModulesDtos;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasketDtoAsync(string Key);
        public Task<BasketDto> CreateOrUpdateBasketDtoAsync(BasketDto basket);
        public Task<bool> DeleteBasketAsync(string Key);
    }
}
