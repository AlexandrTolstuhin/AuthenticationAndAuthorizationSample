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

            var user = new ApplicationUser
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var result = await userManager.CreateAsync(user, "password");

            if (result.Succeeded) 
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
        }
    }
}