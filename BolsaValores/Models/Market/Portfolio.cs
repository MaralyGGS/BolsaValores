using System.Collections.Generic;

namespace BolsaValores.Models.Market
{
    public class Position
    {
        public string Ticker { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal AvgCost { get; set; }
    }

    public class Portfolio
    {
        public string UserId { get; set; } = "demo";
        public decimal CashMXN { get; set; } = 1_000_000m;
        public Dictionary<string, Position> Positions { get; set; } = new();
    }
}
