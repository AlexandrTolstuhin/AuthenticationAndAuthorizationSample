using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorizationSample.JwtBearer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}