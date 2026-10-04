using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModulesDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //Login
        //Take => Email , Password 
        //Return => Email , Token , DisplayName 
        public Task<UserDto> LoginAsync(LoginDto loginDto);

        //Register 
        //Take => Email , Password , DisplayName , UserName , PhoneNumber
        //Return => Email , Token , DisplayName 
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //Check Email 
        //Take => Email
        //Return => Bool

        public Task<bool> CheckEmailAsync(string email);


        //Get Current UserAddress 
        //Take => Email
        //Return => AddressDto

        public Task<AddressDto> GetCurrentUserAddressAsync(string Email);

        //Update Current User Address 
        //Take => Email , AddressDto
        //Return => AddressDto 

        public Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto);


        //Get Current User
        //Take => Email
        //Return => UserDto 

        public Task<UserDto> GetCurrentUserAsync (string email);









    }
}
