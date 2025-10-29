using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolsaValores.Controllers
{
    public class BolsaAmericanaController : Controller
    {
        // GET: BolsaAmericanaController
        public ActionResult Index()
        {
            return View();
        }

    }
}
