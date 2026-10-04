using System.Runtime.CompilerServices;
using System.Text.Json;
using DomanLayer.Contracts;
using E_Commerse.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerse.Web.Extentions
{
    public static class WebApplicationRegistration
    {

        //Here We Used (this WebApplication app) to Access the Services Container From WebApplication  => app.Services   "To Call Services"
        public static async Task<WebApplication> SeedDataBaseAsync (this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var ObjOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjOfDataSeeding.DataSeedAsync();
            await ObjOfDataSeeding.IdentityDataSeedAsync();
            return app;
        }

        //We Dont Need To Call Services Here So We Used (this IApplicationBuilder app)
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }


        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(Options =>
            {
                Options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true,               // Show Duration of Request in Swagger UI

                };

                Options.DocumentTitle = "E-Commerse API Documentation";          // Change Title of Swagger UI Page
                Options.JsonSerializerOptions = new System.Text.Json.JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,              // لما ييجي يحول ملفات ال Json خليه يستخذم ال Camelcase 
                };
                Options.DocExpansion(DocExpansion.None);        // List , Full , None  ===> To Expand EndPOints n Swagger UI

                Options.EnableFilter();                          // Filer Displayed Controllers in Swagger UI

                Options.EnablePersistAuthorization();              // Show Token Box ظهر البوكس بس البيوكس نفسه مش شغال 
                                                                   // then Apply two Services 
                                                                   // AddSecurityDefinition & AddSecurityRequirement in ServicesRegistration Class in AddSwaggerGen() 

            });



            return app;
        }
    }
}


//Notic That U Can Use the Parameter( this WebApplication app) or ( this IApplicationBuilder app)
// Options => {}  <= لو ختاخذ كذا امر حط ال 



// ال .Services is a Property inside WebApplication Only 
//الانترفيس بيشوف الحاجات اللي تخصه بس حتي لو  ورث منه كلاس فيه ميثوذس زياذه واتعمله Assign بيه