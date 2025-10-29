using System;

namespace BolsaValores.Models.Market
{
    public class Trade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Ticker { get; set; } = default!;
        public Guid BuyOrderId { get; set; }
        public Guid SellOrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    }
}
