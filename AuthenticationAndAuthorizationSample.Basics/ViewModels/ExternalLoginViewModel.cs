using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorizationSample.Basics.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}