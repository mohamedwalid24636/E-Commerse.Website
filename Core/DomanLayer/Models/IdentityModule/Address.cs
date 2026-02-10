using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.IdentityModule
{
    public class Address
    {
        public int Id {  get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = default!; // FK => (Unique Index)
    }
}
