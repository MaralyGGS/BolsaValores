using System.Collections.Generic;
using BolsaValores.Models.Market;

namespace BolsaValores.Data
{
    public static class SeedData
    {
        public static IEnumerable<Stock> GetStocks() => new List<Stock>
        {
            new Stock { Ticker = "AMXL",       Name = "América Móvil S.A.B. de C.V.",         ReferencePrice = 19.65m },
            new Stock { Ticker = "WALMEX",     Name = "Walmart de México S.A.B. de C.V.",     ReferencePrice = 73.20m },
            new Stock { Ticker = "BIMBOA",     Name = "Grupo Bimbo S.A.B. de C.V.",           ReferencePrice = 45.10m },
            new Stock { Ticker = "GMEXICOB",   Name = "Grupo México S.A.B. de C.V.",          ReferencePrice = 99.80m },
            new Stock { Ticker = "FEMSAUBD",   Name = "FEMSA S.A.B. de C.V.",                 ReferencePrice = 180.00m },
            new Stock { Ticker = "ALSEA",      Name = "Alsea S.A.B. de C.V.",                 ReferencePrice = 52.00m },
            new Stock { Ticker = "CEMEXCPO",   Name = "Cemex S.A.B. de C.V.",                 ReferencePrice = 18.90m },
            new Stock { Ticker = "KOFUBL",     Name = "Coca-Cola FEMSA S.A.B. de C.V.",       ReferencePrice = 127.50m },
            new Stock { Ticker = "PE&OLES",    Name = "Industrias Peñoles S.A.B. de C.V.",    ReferencePrice = 244.30m },
            new Stock { Ticker = "TLEVISACPO", Name = "Grupo Televisa S.A.B.",                ReferencePrice = 13.20m },
            new Stock { Ticker = "LIVEPOLC-1", Name = "El Puerto de Liverpool S.A.B. de C.V.", ReferencePrice = 88.20m },
            new Stock { Ticker = "ASURB",      Name = "Grupo Aeroportuario del Sureste S.A.B. de C.V.", ReferencePrice = 567.90m },
            new Stock { Ticker = "BBAJIOO",    Name = "Banco del Bajío S.A.",                 ReferencePrice = 70.10m },
            new Stock { Ticker = "GAPB",       Name = "Grupo Aeroportuario del Pacífico S.A.B. de C.V.", ReferencePrice = 290.40m },
            new Stock { Ticker = "OMAB",       Name = "Grupo Aeroportuario del Centro Norte S.A.B. de C.V.", ReferencePrice = 210.70m }
        };
    }
}
