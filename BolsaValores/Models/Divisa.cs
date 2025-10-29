
using System.Collections.Generic;

namespace BolsaValores.Models
{
    public class Divisa
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal ValorActual { get; set; }
        public List<decimal> ValoresDia { get; set; }
        public List<decimal> ValoresSemana { get; set; }
        public List<decimal> ValoresMes { get; set; }
        public List<decimal> ValoresAnio { get; set; }

    }
}
