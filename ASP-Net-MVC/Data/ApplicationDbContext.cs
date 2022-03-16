using ASP_Net_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppAddress> Addresses { get; set; }
        public DbSet<AppUserAddress> UserAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserAddress>()
                .HasKey(c => new {c.UserId, c.AddressId});

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey("LoginProvider", "ProviderKey");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey("UserId", "RoleId");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey("UserId", "LoginProvider", "Name");
        }

    }
}