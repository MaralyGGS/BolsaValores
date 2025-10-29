using System.Collections.Concurrent;
using BolsaValores.Models.Market;

namespace BolsaValores.Services
{
    public class PortfolioService
    {
        private readonly ConcurrentDictionary<string, Portfolio> _portfolios = new();

        public Portfolio Get(string userId) => _portfolios.GetOrAdd(userId, id => new Portfolio { UserId = id });

        public void ApplyFill(string buyerId, string sellerId, string ticker, int qty, decimal price)
        {
            var pb = Get(buyerId);
            pb.CashMXN -= qty * price;
            if (!pb.Positions.TryGetValue(ticker, out var posB))
                pb.Positions[ticker] = posB = new Position { Ticker = ticker, Quantity = 0, AvgCost = 0m };
            var newQty = posB.Quantity + qty;
            posB.AvgCost = (posB.AvgCost * posB.Quantity + price * qty) / newQty;
            posB.Quantity = newQty;

            var ps = Get(sellerId);
            ps.CashMXN += qty * price;
            if (!ps.Positions.TryGetValue(ticker, out var posS)) return;
            posS.Quantity -= qty;
            if (posS.Quantity <= 0) ps.Positions.Remove(ticker);
        }
    }
}
