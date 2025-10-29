using System.Collections.Concurrent;
using System.Collections.Generic;
using BolsaValores.Models.Market;

namespace BolsaValores.Services
{
    public class MarketDataService
    {
        public IReadOnlyDictionary<string, Stock> Stocks => _stocks;
        private readonly Dictionary<string, Stock> _stocks = new();
        private readonly ConcurrentDictionary<string, OrderBook> _books = new();

        public MarketDataService(IEnumerable<Stock> seed)
        {
            foreach (var s in seed) _stocks[s.Ticker] = s;
        }

        public OrderBook GetBook(string ticker)
            => _books.GetOrAdd(ticker, t => new OrderBook(t));
    }
}
