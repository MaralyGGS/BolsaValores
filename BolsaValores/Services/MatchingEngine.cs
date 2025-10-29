using System.Collections.Generic;
using System.Linq;
using BolsaValores.Models.Market;

namespace BolsaValores.Services
{
    public class MatchingEngine
    {
        private readonly MarketDataService _md;
        private readonly PortfolioService _pf;
        private readonly Dictionary<string, List<Trade>> _tapes = new();

        public MatchingEngine(MarketDataService md, PortfolioService pf)
        { _md = md; _pf = pf; }

        public IEnumerable<Trade> GetRecentTrades(string ticker, int take = 50)
        {
            if (!_tapes.TryGetValue(ticker, out var list)) return Enumerable.Empty<Trade>();
            return list.TakeLast(take);
        }

        public (Order accepted, List<Trade> fills) Submit(Order order)
        {
            var book = _md.GetBook(order.Ticker);
            var fills = new List<Trade>();

            if (order.Type == OrderType.Market)
            {
                MatchAgainstOpposite(book, order, fills, crossAtLimitPrice: false);
                order.Status = order.LeavesQty > 0 ? OrderStatus.PartiallyFilled : OrderStatus.Filled;
                return (order, fills);
            }

            MatchAgainstOpposite(book, order, fills, crossAtLimitPrice: true);

            if (order.LeavesQty > 0)
            {
                book.Add(order);
                order.Status = fills.Count > 0 ? OrderStatus.PartiallyFilled : OrderStatus.New;
            }
            else order.Status = OrderStatus.Filled;

            return (order, fills);
        }

        private void MatchAgainstOpposite(OrderBook book, Order incoming, List<Trade> fills, bool crossAtLimitPrice)
        {
            var opposite = incoming.Side == Side.Buy ? book.Asks.ToList() : book.Bids.ToList();
            foreach (var resting in opposite)
            {
                if (incoming.LeavesQty <= 0) break;

                bool crosses = incoming.Type == OrderType.Market
                    || (incoming.Side == Side.Buy && resting.Price <= incoming.Price)
                    || (incoming.Side == Side.Sell && resting.Price >= incoming.Price);

                if (!crosses && crossAtLimitPrice) continue;
                if (!crosses && !crossAtLimitPrice) break;

                int qty = System.Math.Min(incoming.LeavesQty, resting.LeavesQty);
                decimal px = resting.Price;

                var buyer = incoming.Side == Side.Buy ? incoming.UserId : resting.UserId;
                var seller = incoming.Side == Side.Sell ? incoming.UserId : resting.UserId;
                _pf.ApplyFill(buyer, seller, incoming.Ticker, qty, px);

                incoming.LeavesQty -= qty;
                incoming.FilledQty += qty;
                incoming.AvgFillPrice = Wap(incoming.AvgFillPrice, incoming.FilledQty - qty, px, qty);

                resting.LeavesQty -= qty;
                resting.FilledQty += qty;
                resting.AvgFillPrice = Wap(resting.AvgFillPrice, resting.FilledQty - qty, px, qty);

                if (resting.LeavesQty == 0) { resting.Status = OrderStatus.Filled; book.Remove(resting); }
                else resting.Status = OrderStatus.PartiallyFilled;

                var trade = new Trade
                {
                    Ticker = incoming.Ticker,
                    BuyOrderId = (incoming.Side == Side.Buy) ? incoming.Id : resting.Id,
                    SellOrderId = (incoming.Side == Side.Sell) ? incoming.Id : resting.Id,
                    Quantity = qty,
                    Price = px
                };
                RecordTrade(trade);
                fills.Add(trade);
            }
        }

        private void RecordTrade(Trade t)
        {
            if (!_tapes.TryGetValue(t.Ticker, out var list))
                _tapes[t.Ticker] = list = new List<Trade>();
            list.Add(t);
            if (list.Count > 500) list.RemoveRange(0, list.Count - 500);
        }

        private static decimal Wap(decimal prevWap, int prevQty, decimal px, int qty)
            => (prevWap * prevQty + px * qty) / (prevQty + qty);
    }
}
