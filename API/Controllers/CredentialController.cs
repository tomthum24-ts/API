using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CredentialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
