using System.Text.Json;
using DomanLayer.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;

namespace E_Commerse.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next , ILogger<CustomExceptionHandlerMiddleWare> _logger)
        {
            this._next = next;
            logger = _logger;
        }

        public async Task InvokeAsync (HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);     //Error When Internal Server Error Occurs in Next MiddleWares states Code => 500


                #region Method Content
                //if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                //{
                //    var Response = new ErrorToReturn()
                //    {
                //        StatusCode = StatusCodes.Status404NotFound,
                //        Message = $"EndPiont {httpContext.Request.Path} Is Not Found"
                //    };
                //    await httpContext.Response.WriteAsJsonAsync(Response);
                //    //httpContext.Response.ContentType = "application/json";
                //} 
                #endregion

                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "SomeThing Went Wrong");
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Status Code For Response 


            //httpContext.Response.StatusCode = 500;
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;      

            //Create Response Error Object
            var Response = new ErrorToReturn()
            {
                Message = ex.Message
            };


             Response.StatusCode = ex switch
            {

                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException ex_BadRequestException => GetBadRequestException(ex_BadRequestException , Response),        // (U Have ex here )
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = Response.StatusCode;

            //Set Content Type For Response 
            httpContext.Response.ContentType = "application/json";                                   //Default Text/plain
                                                                                                     //But We Dont Need It We Used WriteJason 
                




            //Return Object As Jason 
            //var ResponseToReturn = JsonSerializer.Serialize(Response);
            //await httpContext.Response.WriteAsync(ResponseToReturn);


            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestException(BadRequestException ex_BadRequestException, ErrorToReturn response)
        {
            response.Errors = ex_BadRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"EndPiont {httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
                //httpContext.Response.ContentType = "application/json";
            }
        }
    }
}


//أول catch يقابل الـ Exception وهو طالع لفوق هو اللي هيتنفذ
// فوقه مش هيشوف الـ Exception أصلاًواللي