using Microsoft.AspNetCore.Mvc;
using BolsaValores.Services;

namespace BolsaValores.Controllers
{
    [ApiController]
    [Route("api/market")]
    public class MarketController : ControllerBase
    {
        private readonly MarketDataService _md;
        private readonly MatchingEngine _me;

        public MarketController(MarketDataService md, MatchingEngine me)
        { _md = md; _me = me; }

        [HttpGet("stocks")] public IActionResult Stocks() => Ok(_md.Stocks.Values);

        [HttpGet("book/{ticker}")]
        public IActionResult Book(string ticker)
        {
            var book = _md.GetBook(ticker);
            return Ok(new
            {
                Ticker = ticker,
                Bids = book.Bids.Select(o => new { o.Price, o.LeavesQty, o.Timestamp }),
                Asks = book.Asks.Select(o => new { o.Price, o.LeavesQty, o.Timestamp })
            });
        }

        [HttpGet("tape/{ticker}")] public IActionResult Tape(string ticker) => Ok(_me.GetRecentTrades(ticker, 100));
    }
}
