using System.Text;
using E_Commerse.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace E_Commerse.Web.Extentions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,            // لنهي جزء في ال Request اللي هبعت فيه التوكن
                    Name = "Authorization",                   // اسم ال باراميتر اللي هبتبعب 
                    Type = SecuritySchemeType.ApiKey,         //Api Key مش Two Factor Authentication
                    Scheme = "Bearer",
                    Description = "Enter 'Bearer' => 'Space' => 'Token' "
                });


                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                       new string [] {}
                    }

                });







            });
         
            return Services;
        }


        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
             {
                 Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;
             });
            return Services;
        }




        public static IServiceCollection AddJWTService(this IServiceCollection Service, IConfiguration configuration)
        {
            Service.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;    // Bearer ==> Set Values To Authentication Schema & Authentication Challenge 
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"])),

                };
            });
            return Service;

        }
    }
}


// JWT Token Validation => هنا مبعملش اعادة تكوين كامله للتوكن !
// بيعمل فقط اعادة توليد لل Signiture (Secret Key , Algorithm Type) ثم مقارنة بين ال Sig الي جاي واللي انا ولدته 
// ثم مقارنة لل Issuer , Audience , ...



// ملحوظه اي حد معاه التوكن يقدر يشوف ال داتا اللي جواه من غير Secret Key 