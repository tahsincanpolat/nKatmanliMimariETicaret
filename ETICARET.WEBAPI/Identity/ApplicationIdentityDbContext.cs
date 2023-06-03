using ETICARET.WEBAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.WEBAPI.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
