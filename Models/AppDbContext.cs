using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BinaAz.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
            );

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    UserName = "Ehliman.admin",
                    Name = "Ehliman",
                    Surname = "Mukhtarli",
                    PasswordHash = hasher.HashPassword(null, "Admin.1234"),
                    Email = "ehliman270@gmail.com",
                    EmailConfirmed = true,
                    NormalizedUserName = "EHLIMAN.ADMIN",
                    NormalizedEmail = "EHLIMAN@&)@GMAIL.COM",
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            // No need to seed IdentityUserRole manually. It will be managed by Identity.
        }
    }
}
