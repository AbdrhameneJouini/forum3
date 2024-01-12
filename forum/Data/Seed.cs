using forum.Data;
using forum.Models;
using Microsoft.AspNetCore.Identity;

public class Seed
{
    public static async Task SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ForumDbContext>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<User>
                {
                    new User { UserName = "Admin 1", Admin = true, Email = "admin1@admin.com", EmailConfirmed = true, CheminAvatar = "/" },
                    new User { UserName = "Admin 2", Admin = true, Email = "admin2@admin.com", EmailConfirmed = true, CheminAvatar = "/" },
                    new User { UserName = "User 1", Email = "user1@user.com", EmailConfirmed = true, CheminAvatar = "/" },
                    new User { UserName = "User 2", Email = "user2@user.com", EmailConfirmed = true, CheminAvatar = "/" }
                });

            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            // Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            // Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Admin 1
            var admin1User = await userManager.FindByEmailAsync("admin1@admin.com");
            if (admin1User == null)
            {
                var newAdmin1User = new User
                {
                  
                    UserName = "admin1",
                    Email = "admin1@admin.com",
                    EmailConfirmed = true,
                    Admin = true,
                  
                };
                await userManager.CreateAsync(newAdmin1User, "Abdou");
                await userManager.AddToRoleAsync(newAdmin1User, UserRoles.Admin);
            }

            // Admin 2
            var admin2User = await userManager.FindByEmailAsync("admin2@admin.com");
            if (admin2User == null)
            {
                var newAdmin2User = new User
                {
                   
                    UserName = "admin2",
                    Email = "admin2@admin.com",
                    EmailConfirmed = true,
                    Admin = true,
                 
                    
                };
                await userManager.CreateAsync(newAdmin2User, "Admin");
                await userManager.AddToRoleAsync(newAdmin2User, UserRoles.Admin);
            }

            // User 1
            var user1 = await userManager.FindByEmailAsync("user1@user.com");
            if (user1 == null)
            {
                var newUser1 = new User
                {
                    
                    UserName = "user1",
                    Email = "user1@user.com",
                    EmailConfirmed = true,
               
                    Admin = false
                };
                await userManager.CreateAsync(newUser1, "Abdou");
                await userManager.AddToRoleAsync(newUser1, UserRoles.User);
            }

            // User 2
            var user2 = await userManager.FindByEmailAsync("user2@user.com");
            if (user2 == null)
            {
                var newUser2 = new User
                {
                 
                    UserName = "user2",
                    Email = "user2@user.com",
                    EmailConfirmed = true,
                    CheminAvatar = "/",
             
                };
                await userManager.CreateAsync(newUser2, "User");
                await userManager.AddToRoleAsync(newUser2, UserRoles.User);
            }
        }
    }




}
