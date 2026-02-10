using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exceptions;
using DomanLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObjects.BaketModulesDtos;
using Stripe;
using Product = DomanLayer.Models.ProductModule.Product;
namespace Service
{
    public class PaymentService(IConfiguration _configuration,
        IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork
        ,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentInentAsync(string BasketId)
        {
            // Install Strip.Net Package & Configure IntentKey 
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            //Get Basket By BasketId To Add Intent Key To It
            var Basket = await basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);
            var ProductRepo = unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            // Get Cost Of Delivery Method 
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);     // طريقة جاهزة اثرو بيها اكسيبشن بذل ما اعمل واحذ جذيذ
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value) ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);  // We Wrote "Value" Because GetByIdAsync Takes non-Nullable string and we send Nullable string
            Basket.shippingPrice = DeliveryMethod.Price;
           
            //Get Amount = Get Product(Price* Quantity) + DeliveryMethod Cost 
            var BasketAmount =(long) ( Basket.Items.Sum(i => i.Price * i.Quantity) + DeliveryMethod.Price ) * 100;    //ضربت في 100 عشان ميضيعش مني ارقام صغيره وانا بحول لذولار  
                                                                                                                      
            //Create PaymentIntent [ Create - Update ] 
            var PaymentIntentService = new PaymentIntentService();
            if (Basket.paymentIntentId is null)  //Create
            {
                var CreateOptions = new PaymentIntentCreateOptions
                {
                    Amount = BasketAmount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentIntentService.CreateAsync(CreateOptions);  // PaymentIntent Created !! (PaymentIntentId & ClientSecret )
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else                                //Update
            {
                var UpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount
                };
                await PaymentIntentService.UpdateAsync(Basket.paymentIntentId, UpdateOptions);

                //paymentIntentId , clientSecret  متسجلين قبل كذه لما عملت create  فمش محتاج اكتبهم تاني 
            }

            await basketRepository.CreateOrUpdateBasketAsync(Basket);  // Update Basket With PaymentIntentId & ClientSecret & ShippingPrice
            return _mapper.Map<BasketDto>(Basket);

        }
    }
}
//خذ بالك انا استذعيت ال باسكت 
// عملت Override علي سعر المنتجات بتاعته عشان انا مش واثق في بتا الفرونت 
// جبت تكلفة ال توصيل وكتبتها في الباسكت 
//كونت ال Payment Intnet
// ثم اضف للباسكت ال PaymentIntent Id & Client Secret



// use Stripe Test Card Number Site 