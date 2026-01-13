using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobsCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
