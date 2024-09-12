using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed Roles (User, Admin, SuperAdming)
            var adminRoleID = "1a33803e-d84c-420e-b290-ff45849ff2f8";
            var superAdminRoleID = "c67dfc1f-b392-4909-8e08-b30f53d3c643";
            var userRoleID = "c80d0850-e9a6-4a04-a732-9db0ea33dfb3";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id = adminRoleID,
                    ConcurrencyStamp = adminRoleID
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id = superAdminRoleID,
                    ConcurrencyStamp = superAdminRoleID
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id = userRoleID,
                    ConcurrencyStamp = userRoleID
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdmingUser
            var superAdminId = "d136baaa-7d9a-490b-a51b-e9bdca1401e0";
            var superAdminUser = new IdentityUser
            {
                UserName = "superAdmin@bloggy.com",
                Email = "superAdmin@bloggy.com",
                NormalizedEmail = "superAdmin@bloggy.com".ToUpper(),
                NormalizedUserName = "superAdmin@bloggy.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to SuperAdmingUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleID,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleID,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleID,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
