using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationAndAuthorizationSample.Basics.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorizationSample.Basics.Extensions
{
    public static class DatabaseInitializeExtensions
    {
        public static async Task InitDatabase(this IServiceProvider provider)
        {
            var userManager = provider.GetService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                FirstName = "FirstName 1",
                LastName = "LastName 1"
            };

            var result = await userManager.CreateAsync(adminUser, "password");

            if (result.Succeeded) 
                await userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Role, "Administrator"));

            var managerUser = new ApplicationUser
            {
                UserName = "manager",
                FirstName = "FirstName 2",
                LastName = "LastName 2"
            };

            result = await userManager.CreateAsync(managerUser, "password");

            if (result.Succeeded)
                await userManager.AddClaimAsync(managerUser, new Claim(ClaimTypes.Role, "Manager"));
        }
    }
}