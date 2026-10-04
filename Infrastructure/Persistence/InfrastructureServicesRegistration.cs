

using DomanLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.Identity;
using Service;
using ServiceAbstraction;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
        {

                                                 /* Store DbContext Service */

            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });
            
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork , UnitOfWork>();
            Services.AddScoped<IBasketRepository,BasketRepository>();
            Services.AddScoped<ICachesRepository,CacheRepository>();
            Services.AddScoped<IMachineReportService, MachineReportService>();
            /* Redis InMemmory Service */

            // Singleton => عشان بخزن مع الميموري فمحتاج اوبجكت واحذ يعيش طول فترة ما الابليكيشن شغال  
            Services.AddSingleton<IConnectionMultiplexer>( (ServiceProvider) =>   // مشتغلناش Async عشان ال Version اللي هو شغال بيها مش Async + اننا مستخذمناش ال service providerobject
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString")!);
            });
           


                                              /* Identity DbContext Service */
            
            
            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            //Allow Dependancy injection for UserManager , RoleManager
            Services.AddIdentityCore<ApplicationUser>((Options) =>             
            {   
                //Options.User.RequireUniqueEmail = true;
                //Options.Password.RequireNonAlphanumeric = false;
                //Options.SignIn.RequireConfirmedEmail = true;
                //Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<StoreIdentityDbContext>();    //مين ال DbContext اللي انا بتعامل معاها 

           /*  كنت هستخذم ال الطريقه ذيه لو كنت بكتب في ال program.cs  ,  عشان ال namespace بتاعتها مش موجوذه هنا */
           // builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
           //.AddEntityFrameworkStores<StoreIdentityDbContext>();
           
            return Services;

        }
    }
}


//builder.Services.AddIdentity<ApplicationUser, IdentityRole>( (Options) =>  Options.User.RequireUniqueEmail = true; )
//    .AddEntityFrameworkStores<StoreDbContext>();


//Using => Microsoft.Extension.DependancyInjection  To Get AddIdentity Method     , Not The SameOne In GlobalUsing 




// app. =>WebApplication    , his interface is IApplicationBuiler  