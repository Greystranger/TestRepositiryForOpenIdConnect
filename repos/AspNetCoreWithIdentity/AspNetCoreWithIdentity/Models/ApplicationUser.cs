using Microsoft.AspNetCore.Identity;

namespace AspNetCoreWithIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
    }
}
