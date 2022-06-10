using Microsoft.AspNetCore.Mvc;

namespace ProyectoDia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
