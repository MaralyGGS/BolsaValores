using System.Collections.Generic;
using System.Linq;

namespace BolsaValores.Models.Market
{
    public class OrderBook
    {
        public string Ticker { get; }
        private readonly LinkedList<Order> _bids = new();
        private readonly LinkedList<Order> _asks = new();

        public OrderBook(string ticker) => Ticker = ticker;

        public IEnumerable<Order> Bids => _bids;
        public IEnumerable<Order> Asks => _asks;

        public void Add(Order o)
        {
            o.LeavesQty = o.Quantity;
            if (o.Side == Side.Buy) InsertByPriceTime(_bids, o, desc: true);
            else InsertByPriceTime(_asks, o, desc: false);
        }

        public bool Remove(Order o) => (o.Side == Side.Buy) ? _bids.Remove(o) : _asks.Remove(o);

        private static void InsertByPriceTime(LinkedList<Order> list, Order o, bool desc)
        {
            var node = list.First;
            while (node != null)
            {
                bool goesBefore = desc ? o.Price > node.Value.Price : o.Price < node.Value.Price;
                if (goesBefore) { list.AddBefore(node, o); return; }
                if (o.Price == node.Value.Price)
                {
                    var scan = node;
                    while (scan != null && scan.Value.Price == o.Price) scan = scan.Next;
                    if (scan == null) { list.AddLast(o); return; }
                    list.AddBefore(scan, o); return;
                }
                node = node.Next;
            }
            list.AddLast(o);
        }
    }
}
