using Microsoft.AspNetCore.Identity;

namespace GeekShooping.IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
    }
}
