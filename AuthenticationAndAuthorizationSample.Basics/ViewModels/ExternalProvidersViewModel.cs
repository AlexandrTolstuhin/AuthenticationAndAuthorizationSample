using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace AuthenticationAndAuthorizationSample.Basics.ViewModels
{
    public class ExternalProvidersViewModel
    {
        public string ReturnUrl { get; set; }

        public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }
    }
}