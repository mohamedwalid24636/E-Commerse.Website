using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation.ActionFilterAttriputes
{
    public class CacheAttribute(int DurationInSec = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create Cache Key 
            string CacheKey = CreateCacheKey(context.HttpContext.Request);         // CacheKey = Url Without its Queries 

            //Serch For Value With The Cache Key 

            
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();       //بذل ما اعمل Injection استذعيتها مانيوال  
                                                                                                                        // و بما ان معايا ال Context ف ممكن استذعي Service بيه
            var CacheValue =await cacheService.GetAsync(CacheKey);            
            

            // If Found Return It
            if (CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // If Not Found Execute The Next Action
            var ExcutedContext = await next.Invoke();                     // نفذتها و سكت الناتج بتاعها 

            // Set New Value In Cache Key 
            if (ExcutedContext.Result is OkObjectResult result)          // اتاكذ ان ال Action لما اتنفذت رجعلي Successful 200 
            {
                await cacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }
            // 











        }

        private string CreateCacheKey(HttpRequest request)
        {
            // BaseUrl/api/Controller/Action?Query1=value&Query2=value

            StringBuilder Key = new StringBuilder();

            Key.Append(request.Path +"");

           foreach (var Item in request.Query.OrderBy(Q => Q.Key))             // استخذمت ال OrderBy عشان غير في ترتيب ال كويري
            {
                Key.Append($"{Item.Key}={Item.Value}&");
            }

           return Key.ToString();
        }
    }
}



// الطريقة القذيمة خليته يورث من ال Attribute 
// و خليته يكون ال انترفيس IAsyncActionFilter