using System.Security.Claims;
using AuthenticationAndAuthorizationSample.Basics.Data;
using AuthenticationAndAuthorizationSample.Basics.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthenticationAndAuthorizationSample.Basics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MEMORY");
                })
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Secure/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireAssertion(c => c.User.HasClaim(ClaimTypes.Role, "Administrator"));
                });

                options.AddPolicy("Manager", builder =>
                {
                    builder.RequireAssertion(c => 
                        c.User.HasClaim(ClaimTypes.Role, "Manager") ||
                        c.User.HasClaim(ClaimTypes.Role, "Administrator"));
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}