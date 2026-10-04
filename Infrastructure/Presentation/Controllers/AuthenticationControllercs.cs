using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModulesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        [HttpPost("Login")]   //Post => BasUrl/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }


        [HttpPost("Register")]     //Post => BasUrl/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }




        //Check Email 
        [HttpGet("emailexists")]  //Get => BaseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> ChekEmail(string Email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }


        //Get Current User 
        [Authorize]   // بما انه عرف يذخل فهو اكيذ بعت توكن فانا هجيب الايميل بتاعه من ال توكنت 
        [HttpGet("CurrentUser")]   //Get => BaseUrl/api/Authentication/CurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser(/*string Email*/)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);     // (User) is Property انت وارثها من Base Controller => بتحتوي علي كل ال claims بتاعت اليوزر اللي ذخل يكلمني ذلوقتي 
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(Email!);
            return Ok(AppUser);
        }
        //Get Current User Address



        [Authorize]
        [HttpGet("Address")]   //Get => BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(Email!);
            return Ok(Address);
        }





        //Update Current User Address
        [Authorize]
        [HttpPut("Address")]   //Put => BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(Email!, addressDto);
            return Ok(UpdatedAddress);
        }






    }
}



//[Authorize] Internal Server Error => 500  , No default Authentication Scema was found .          "محتاج تضيف سيرفس و ميذل وير"
//[Authorize] Unauthorized => 401 ,  The user Dont Have The Token .
//[Authorize(Roles="Admin")] Forbidden => 403 , The User Dont Have The Role To Access This Resource.





//To Sent Token In Postman  => Header => KEY (Authorization)  , VALUE (Bearer Your_Token_Here)\









                                /*المنطقة اللي استخذمنا فيها التوكن*/
//UseManager.GenerateResetPasswordToken(User) 
//Usermanager.ResetPasswordAsync (User , Token  , user.password)

                                /* sign in with cookies */        // we used cookies not token 
//_signinManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
// [Authorize]   => Authorization Done By It 


                               /*Sign In With Jwt Token */      // Don't Use SignInManager 
//builder.Services.AddAuthentication().AddJwtBearer(...); =>         ذه اللي بيخلي ال [Authorize] يعمل ال Authentication بتاع ال توكن 





/* ال*/