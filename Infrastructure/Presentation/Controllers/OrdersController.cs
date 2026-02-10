using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilterAttriputes;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrdersDtos;

namespace Presentation.Controllers
{

    [Authorize]       // الكنترولر كله authorized
    public class OrdersController(IServiceManager _serviceManager) :ApiBaseController
    {
          
        
        [HttpGet]         //Get BaseUrl/api/Orders
        //[ResponseCache]
        [Cache]         //Cache For 90 Seconds As Default
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }




        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(Order);
        }

        //Get Delivery Methods 

        [AllowAnonymous]         //allow unauthorized
        [HttpGet("DeliveryMethods")] //Get BaseUrl/api/Orders/DeliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMehtods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMehtods);
        }
        //Get All Orders By Email 
     
     
        //Get Order By Id 
        
        [HttpGet("{Id:guid}")]         //Get BaseUrl/api/Orders/OrderId
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(Id);
            return Ok(Order);

        }
    }

}
