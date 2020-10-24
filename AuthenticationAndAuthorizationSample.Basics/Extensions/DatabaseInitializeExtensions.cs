using System;
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

            await userManager.CreateAsync(user, "password");
        }
    }
}