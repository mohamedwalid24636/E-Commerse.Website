using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityModulesDtos
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? UserName { get; set; }
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string? PhoneNumber { get; set; }

    }
}
