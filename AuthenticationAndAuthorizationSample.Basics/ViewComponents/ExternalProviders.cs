using AuthenticationAndAuthorizationSample.Basics.Entities;
using AuthenticationAndAuthorizationSample.Basics.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorizationSample.Basics.ViewComponents
{
    public class ExternalProviders : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExternalProviders(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            return View(new ExternalProvidersViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }
    }
}