using Microsoft.AspNetCore.Mvc;
using BolsaValores.Models.Market;
using BolsaValores.Services;

namespace BolsaValores.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly MatchingEngine _engine;
        public OrdersController(MatchingEngine engine) => _engine = engine;

        public record NewOrderDto(string UserId, string Ticker, Side Side, OrderType Type, decimal Price, int Quantity);

        [HttpPost]
        public IActionResult Submit(NewOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                Ticker = dto.Ticker,
                Side = dto.Side,
                Type = dto.Type,
                Price = dto.Price,
                Quantity = dto.Quantity
            };
            var (accepted, fills) = _engine.Submit(order);
            return Ok(new { accepted, fills });
        }
    }
}
