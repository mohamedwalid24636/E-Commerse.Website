
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



            #region Validation Configuration
            //builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            //{
            //    Options.InvalidModelStateResponseFactory = (Context) =>
            //    {
            //        var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
            //        .Select(M => new ValidationError()
            //        {
            //            Field =M.Key,
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
            app.MapControllers();

            #endregion



            app.Run();
        }
    }
}




//ملحوظة مهمة:

//async 
//قسّم الدالة وخلّيها قابلة للتوقيف والاستكمال