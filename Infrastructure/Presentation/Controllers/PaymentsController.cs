using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BaketModulesDtos;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager): ApiBaseController
    {
        //Create Or Update PaymentIntent Id 
        [Authorize]
        [HttpPost("{BasketId}")] //Post BaseUrl/api/Payments/5242
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentInentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
