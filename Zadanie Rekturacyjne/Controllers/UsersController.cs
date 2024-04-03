using Microsoft.AspNetCore.Mvc;

namespace Zadanie_Rekturacyjne.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
