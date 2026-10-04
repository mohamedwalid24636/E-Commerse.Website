using DomanLayer.Contracts;
using DomanLayer.Models.IdentityModule;
using E_Commerse.Web.CustomMiddleWares;
using E_Commerse.Web.Extentions;
using E_Commerse.Web.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.ProductProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;

//Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection   in package manager console
namespace E_Commerse.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Add Services To Container

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.Controllers.ProductsController).Assembly);  // ===> Why ?!


            builder.Services.AddSwaggerServices();                              //Extension Method From Web Layer


            builder.Services.AddInfrastructureServices(builder.Configuration);  //Extension Method From Persistence Layer   
            builder.Services.AddApplicationServices();                          //Extension Method From Service Layer


            builder.Services.AddWebApplicationServices();                       //Extension Method From Web Layer

            builder.Services.AddJWTService(builder.Configuration);


            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("AllowAll", PolicyBuilder =>
                {
                    PolicyBuilder.AllowAnyOrigin();
                    PolicyBuilder.AllowAnyMethod();
                    PolicyBuilder.AllowAnyHeader();
                });
            });



            #region Bad Reuest Validation Error Configuration
            //builder.Services.Configure<ApiBehaviorOptions>((Config) =>
            //{
            //    Config.InvalidModelStateResponseFactory = (Context) =>
            //    {
            //        var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
            //        .Select(M => new ValidationError()
            //        {
            //            Field = M.Key,
            //            Errors = M.Value.Errors.Select(E => E.ErrorMessage)
            //        });
            //        var Response = new ValidationErrorToReturn()
            //        {
            //            ValidationErrors = Errors
            //        };


            //        return new BadRequestObjectResult(Response);
            //    };

            //});


            #endregion

            // خد بالك ان ال ApiController عبارة عن Service + هي اللي بتعمل Validation on data Type
            // Config => معناها ستأذنك انا هعدل 

            // InvalidModelStateResponseFactory => فريم وورك بينده عليها لما ال ModelState.IsValid == false 
            // it is Func<ActionContext , IActionResult> !!

            // Context  .HttpContext , .ModelState , .RouteData
            // modelstate => Dictionary <string , ModelstateEntry>
            // ModelStateEntry .ModelError                               "Errors Type Is ModelError "
            // ModelError  .Errors , Exceptions 







            #endregion



            //var app = builder.Build();

            //using var scope = app.Services.CreateScope();
            //var ObjOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            //await ObjOfDataSeeding.DataSeedAsync();

            var app = builder.Build();

            await app.SeedDataBaseAsync();                                       //Extension Method From Web Layer






            // Configure the HTTP request pipeline.

            //app.Use(  async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();                            // بما اناه Await السطرين اللي تحتيها بيستنوا لغاية ما السطر يخلص 
            //    Console.WriteLine("Waiting Response");
            //    Console.WriteLine(RequestContext.Response.StatusCode);
            //}
            //);


            //========================================================================================



            #region Cofigure The HTTP Request Pipline
            //app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            app.UseCustomExceptionMiddleWare();                             //Extension Method From Web Layer





            //========================================================================================

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();                            
            //}


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();                               //Extension Method From Web Layer
            }





            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");                    // اسم ال Cor اللي انتا ضفتها 
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();                 // بيودي ال request ل endpoint معينة

            #endregion



            app.Run();
        }
    }
}




//ملحوظة مهمة:

//async 
//قسّم الدالة وخلّيها قابلة للتوقيف والاستكمال



//UseStaticFiles() بنخليك لما تكتب Url ميمشيش فيه علي اساس انه Api 
//بتخليك يروح يذور في ال wwwroot 












// <-- Program LifeCycle -->

//1- Create Models 
//2- Create DbContext Connection
//3- Create Repository
//4- Create UnitOfWork
//5- Create Dtos & IMapper         => Because U Will Need to Craete Service 
//6- Create Services               => Here U Will Will Initiate <TEntity,TKey>
//7- 







// Async => ابذأ ال فانكشن و متستناش نتيجيتها وبالتالي نفذ السطرين اللي تحتها كمان "Fire and Forget"
// await => لا استنه نتيجه الفانكشن ترجع و بعذها نفذ السطرين اللي تحت عشان نتيجتهم معتمذه عليها 
// await => هواللي بيحرر ال thread 
// thread => هو اللي بينفذ سطور الكوذ في الفانكشن 
//     <-- the Story -->
//استخذم createasync بمعني ابذأالفانكشن و متستناش نتيجيتها وانزل نفذ باقي السطور وبالتاليممكن يحصل مشكله لان تنفيذ باقي السطور كان معتمذ علي نتيجه الفانكشن ذيه 
// طب يا سيدي مش هستخدم Async يعني هشتغل بالفانكشن العادية : ساعتها ال thread هيفضل مستني لغاية ما الفانكشن تخلص يعني اتعطل 
// طب يا سيدي هستخدم await create async ساعتها هيبدأ ال function بسبب async و باقي السطور مش هتتنفذ بسبب await و الthread هيتحرر بسبب await برضو 




//app.use() => هي ذيه ال middlewares 



//To Make MiddleWare =>
//1- create Middleware class with public constructor with parameter of type RequestDelegate "اللي هيشيل ال Next Request "
//2- public method Invoke OR InvokeAsync (HttpContext) الباراميتر اللي ماسك ال request الحالية + Return Task






