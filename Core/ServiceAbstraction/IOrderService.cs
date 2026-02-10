using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrdersDtos;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //Create Order
        //Takes   => BasketId,Shipping Address , DeliveryMethodId , Custmer Email
        //Returns => Id , UserName , OrderDate , Items (ProductName , PictureUrl , Price , Quantity)
        //,Address , DeliveryMethod Name ,OrderStatus Value,  Subtotal , Total Price

        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email);

        //Get Delivery Method 
        Task <IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        //Get All Orders 
        Task <IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);

        //Get Order By Id

        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);
    }
}
