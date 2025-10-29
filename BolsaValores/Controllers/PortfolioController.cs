using Microsoft.AspNetCore.Mvc;
using BolsaValores.Services;

namespace BolsaValores.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _pf;
        public PortfolioController(PortfolioService pf) => _pf = pf;

        [HttpGet("{userId}")]
        public IActionResult Get(string userId) => Ok(_pf.Get(userId));
    }
}
