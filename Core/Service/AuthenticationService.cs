using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Exceptions;
using DomanLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModulesDtos;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration , IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;

        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            //_userManager.FindByEmailAsync(email);   => مستخذمتهاش عشان هترجلي اليوزر من غير ال navigation property



            var User = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email); ;
               
             return new UserDto()
            {
                DisplayName = User.DisplayName,
                Token = await CreateTokenAsync(User),
                UserName = User.UserName
            };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(U => U.Email == Email)   // i can make filteration inside FirstOrDefault() instead of where ()
            ?? throw new UserNotFoundException(Email);

            return _mapper.Map<AddressDto>(User.Address);    // عشان ال فرونت مش مهنذلها



            //if (User.Address is not null)
            //    return _mapper.Map<AddressDto>(User.Address);
            //else
            //    throw new AddressNotFoundException(User.UserName);
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
           var User = await _userManager.Users.Include(U=> U.Address).FirstOrDefaultAsync(U => U.Email == Email) ?? throw new UserNotFoundException(Email);
           
            if(User.Address is not null)  // Update 
                User.Address = new Address()
                {
                    City = addressDto.City,
                    Country = addressDto.Country,                //User.Address = _mapper.Map<Address>(addressDto);    => مييييينفعش استخذمها             
                    Street = addressDto.Street,                 //=> لانك عشان تعمل update محتاج تغير ال Object State !  AutoMapper Dont Change Objeect State to Update 
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName
                };

           
            else                          // Create
                User.Address = _mapper.Map<Address>(addressDto); 
            
            await _userManager.UpdateAsync(User);    //  => عشان اعمل update لازم اعمل SaveChanges

            return _mapper.Map<AddressDto>(User.Address);
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
                throw new UserNotFoundException(loginDto.Email);
            var IsValidPassword = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsValidPassword)
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User),
                    UserName = User.UserName
                };
            else
            {
                throw new UnauthorizedException();
            }


        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
            };


            var Result = await _userManager.CreateAsync(User, registerDto.Password);         //======> Review
            if (Result.Succeeded)
                return new UserDto()
                {
                    UserName = User.UserName,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)

                };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList(); //  Select => Because IEnumrable 

                throw new BadRequestException(Errors);
            }


        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            // Add Claims 
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),                 // ! => means don't worry UserName Can't Be Null , معني انه وصل للمرحلة ذيه انه استحالة يكون null
                new (ClaimTypes.NameIdentifier , user.Id!)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }




            // create Security Key & select Algorithm  

            var Key = _configuration.GetSection("JWTOptions")["SecretKey"];                                                   // "محتاج احول المفتاح لل Binary "
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));     // SymmetricSecurityKey() "بتعمل ال Security Key"         
            var Algorithm = SecurityAlgorithms.HmacSha256;                     //  بتحدد نوع ال Algorithm

            // Create Cerdentials 
            var Cerd = new SigningCredentials(SecurityKey, Algorithm);  // HmacSha256Signature => Algorithm

            // Create Token 

            var Token = new JwtSecurityToken
                (
                  issuer: _configuration["JWTOptions:Issuer"],
                  audience: _configuration["JWTOptions:Audience"],
                  claims: Claims,
                  expires: DateTime.Now.AddHours(1),
                  signingCredentials: Cerd
                );


            // Return Token 
            return new JwtSecurityTokenHandler().WriteToken(Token);


        }
    }
}




//JWT Package => AspNetCore.Authentication.JWTBearer