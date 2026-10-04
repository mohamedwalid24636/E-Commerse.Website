using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exceptions;
using DomanLayer.Models.IdentityModule;
using DomanLayer.Models.OrderModule;
using Product = DomanLayer.Models.ProductModule.Product;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModulesDtos;
using Shared.DataTransferObjects.OrdersDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork , IConfiguration _configuration) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            //Map AddressDto To Get OrderAddress 

            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);

            
            //Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);
            












            // Payment Intent Check Part 
            ArgumentNullException.ThrowIfNull(Basket.paymentIntentId);   // If PaymentIntent Id Not Exist  ,, مينفعش انشئ اورذر من غير ال paymentntent id 


            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            // Get PaymentIntent from Stripe
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.GetAsync(Basket.paymentIntentId);

            // Verify payment succeeded
            if (paymentIntent.Status != "succeeded")
            {
                throw new Exception("Payment has not been completed.");
            }


            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();























            var OrderSpec = new OrderWithPaymentIntentIdSpecifications(Basket.paymentIntentId);  // Search If Any Other Orders Has The Same Payment Intent Id 
            var ExistingOrders = await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrders is not null) OrderRepo.Delete(ExistingOrders);                    // امسح الاورذر القذيم لو لقيت يه نفس ال intent id 
               


            //Create OrderItem List
            var OrderItems = new List<OrderItem>();                          // orderItem = BasketItem 
            var Repo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in Basket.Items)                                 // انا مش واثق فيه فا مش هاخذ ال Data From the Basket Item 
            {                                                                  // خذت ال ذاتا المهمة من ال product that is saved in my database , and i get the rest from the basket item
                var Product = await Repo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                var OrderItem = CreateOrderItem(item, Product);
                OrderItems.Add(OrderItem);
            }


            //Get Delivery Method 
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);


            //Calculate SubTotal


            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);


            //Create Order 

            var order = new Order(Email, OrderAddress,  DeliveryMethod, OrderItems, SubTotal , Basket.paymentIntentId);
            
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            var ReturnedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            ReturnedOrder.deliveryCost = DeliveryMethod.Price;
            return ReturnedOrder;
        }


        private static OrderItem CreateOrderItem(DomanLayer.Models.BaskedModule.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder() { Id = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                Price = Product.Price,
                Quantity = item.Quantity
            };
        }

        

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecification(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync( Spec );
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id , string CurrentUser)
        {
            var Spec = new OrderSpecification(Id, CurrentUser);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            if (Order is null) throw new OrderNotFoundException(Id);
            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }
    }
}
