namespace BolsaValores.Models.Market
{
    public class Stock
    {
        public string Ticker { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal ReferencePrice { get; set; }
        public int LotSize { get; set; } = 1;
        public bool Enabled { get; set; } = true;
    }
}
