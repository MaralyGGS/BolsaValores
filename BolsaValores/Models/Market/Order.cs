using System;

namespace BolsaValores.Models.Market
{
    public enum Side { Buy, Sell }
    public enum OrderType { Limit, Market }
    public enum OrderStatus { New, PartiallyFilled, Filled, Canceled }

    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = "demo";
        public string Ticker { get; set; } = default!;
        public Side Side { get; set; }
        public OrderType Type { get; set; } = OrderType.Limit;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LeavesQty { get; set; }
        public int FilledQty { get; set; }
        public decimal AvgFillPrice { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.New;
    }
}
