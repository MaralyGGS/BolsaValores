using System.Collections.Generic;
using BolsaValores.Models.Market;

namespace BolsaValores.Data
{
    public static class SeedData
    {
        public static IEnumerable<Stock> GetStocks() => new List<Stock>
        {
            new Stock { Ticker = "AMXL",    Name = "América Móvil",   ReferencePrice = 18.50m },
            new Stock { Ticker = "WALMEX*", Name = "WALMEX",          ReferencePrice = 73.20m },
            new Stock { Ticker = "BIMBOA",  Name = "Bimbo A",         ReferencePrice = 45.10m },
            new Stock { Ticker = "GMEXICOB",Name = "Grupo México B",  ReferencePrice = 99.80m },
            new Stock { Ticker = "FEMSAUBD",Name = "FEMSA UBD",       ReferencePrice = 180.00m },
        };
    }
}
