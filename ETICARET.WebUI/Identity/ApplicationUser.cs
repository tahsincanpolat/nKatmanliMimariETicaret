using Microsoft.AspNetCore.Identity;

namespace ETICARET.WebUI.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
