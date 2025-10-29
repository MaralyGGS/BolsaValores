using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolsaValores.Controllers
{
    public class BolsaMexicanaController : Controller
    {
        // GET: BolsaMexicanaController
        public ActionResult Index()
        {
            return View();
        }

    }
}
