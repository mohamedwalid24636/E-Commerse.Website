using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BaketModulesDtos;

namespace Presentation.Controllers
{
    
    public class basketsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get Basket 
        [HttpGet]  // Get => BaseUrl/api/baskets   ?Key=12345
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
           var Basket = await _serviceManager.BasketService.GetBasketDtoAsync(id);
           return Ok(Basket);
        }


        //CreateOrUpdate Basket 

        [HttpPost]  // Post => BaseUrl/api/baskets
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var CreatedOrUpdatedBasket = await _serviceManager.BasketService.CreateOrUpdateBasketDtoAsync(basket);
            return Ok(CreatedOrUpdatedBasket);
        }

        //Delete Basket

        [HttpDelete("{Key}")]  // Delete => BaseUrl/api/baskets/12345
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var IsDeleted = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(IsDeleted);
        }
    }
}
