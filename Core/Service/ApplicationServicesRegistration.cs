using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        //مهمة جذااا الحركة ذيه  
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            //Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            Services.AddScoped<IProductService, ProductService>();  
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IPaymentService, PaymentService>();

            Services.AddScoped<Func<IProductService>>(Provider =>            // He Wait U To Send Function < Takes IServiceProvider , Returns Func<IProductService> > 
                () => Provider.GetRequiredService<IProductService>()         // Provider => ذه الي سيرفس اللي انا بطلبها 
             );                                                            //  لما مسكت ال بروفايذر طلبت منه يحقنلي اوبجكت معين مانيوال 


            Services.AddScoped<Func<IBasketService>>(Provider =>
            () => Provider.GetRequiredService<IBasketService>());

            Services.AddScoped<Func<IOrderService>>(Provider =>
            () => Provider.GetRequiredService<IOrderService>());

            Services.AddScoped<Func<IAuthenticationService>>(Provider =>
            () => Provider.GetRequiredService<IAuthenticationService>());


            Services.AddScoped<Func<IPaymentService>>(Provider =>
            () => Provider.GetRequiredService<IPaymentService>());

            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }
    }
}


// في علاقه بين ال Lazy Impelementation و Factory Delegate ways لحاول تعرفها 