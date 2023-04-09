using Microsoft.AspNetCore.Identity;

namespace ETICARET.WebUI.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var password = configuration["Data:AdminUser:password"];
            var email = configuration["Data:AdminUser:email"];
            var role = configuration["Data:AdminUser:role"];


            if(await userManager.FindByEmailAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));

                var user = new ApplicationUser()
                {
                    UserName = username,
                    Email = email,
                    FullName = "Admin User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user,password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,role);
                }
               
            }

        }
    }
}
