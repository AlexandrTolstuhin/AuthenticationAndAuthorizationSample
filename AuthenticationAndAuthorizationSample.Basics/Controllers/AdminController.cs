using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorizationSample.Basics.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
