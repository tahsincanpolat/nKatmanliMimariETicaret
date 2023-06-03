using Microsoft.AspNetCore.Identity;

namespace ETICARET.WEBAPI.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
