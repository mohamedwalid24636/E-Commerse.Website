using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exceptions;
using DomanLayer.Models.BaskedModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BaketModulesDtos;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {

        public async Task<BasketDto> CreateOrUpdateBasketDtoAsync(BasketDto basket) 
        {
            var CustmerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustmerBasket);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketDtoAsync(basket.Id);
            else
                throw new Exception("Failed To Create Or Update Basket , Sorry");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
          return await _basketRepository.Deletebasketasync(Key);
        }

        public async Task<BasketDto> GetBasketDtoAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }
    }
}
