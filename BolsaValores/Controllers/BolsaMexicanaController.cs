using Microsoft.AspNetCore.Mvc;

namespace BolsaValores.Controllers
{
    public class BolsaMexicanaController : Controller
    {
        public IActionResult Index()
        {
            return View("Simulador");
        }
    }
}
