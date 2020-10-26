using AuthenticationAndAuthorizationSample.Basics.Data;
using AuthenticationAndAuthorizationSample.Basics.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorizationSample.Basics
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = _configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = _configuration["Authentication:Facebook:AppSecret"];
                })
                .AddOAuth("VK", "VK", config =>
                {
                    config.ClientId = _configuration["Authentication:VK:AppId"];
                    config.ClientSecret = _configuration["Authentication:VK:AppSecret"];
                    config.ClaimsIssuer = "VK";
                    config.CallbackPath = new PathString("/signin-vk-token");
                    config.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                    config.TokenEndpoint = "https://oauth.vk.com/access_token";
                    config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                    config.SaveTokens = true;
                    config.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            context.RunClaimActions(context.TokenResponse.Response.RootElement);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Secure/Login";
                //options.AccessDeniedPath = "/Home/AccessDenied";
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