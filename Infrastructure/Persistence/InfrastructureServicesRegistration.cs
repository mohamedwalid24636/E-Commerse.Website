

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
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });
            
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository,BasketRepository>();
            Services.AddScoped<ICachesRepository,CacheRepository>();

            // Singleton => Because of One Object of It Can Handle Many Operations Easlly 
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>   // مشتغلناش Async عشان ال Version اللي هو شغال بيها مش Async 
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
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
              .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return Services;

        }
    }
}


//builder.Services.AddIdentity<ApplicationUser, IdentityRole>( (Options) =>  Options.User.RequireUniqueEmail = true; )
//    .AddEntityFrameworkStores<StoreDbContext>();


//Using => Microsoft.Extension.DependancyInjection  To Get AddIdentity Method     , Not The SameOne In GlobalUsing 