using BolsaValores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolsaValores.Controllers
{
    public class CasaCambioController : Controller
    {
        // GET: CasaCambioController
        public ActionResult Index(string codigo = "USD")
        {
            // Lista simulada de divisas
            var divisas = ObtenerDivisasSimuladas();

            // Seleccionar divisa para mostrar gráfica, por defecto USD
            var divisaSeleccionada = divisas.Find(d => d.Codigo == codigo) ?? divisas[0];

            var model = new CasaDeCambioViewModel
            {
                Divisas = divisas,
                DivisaSeleccionada = divisaSeleccionada,
                Rango = "dia" // rango inicial para gráfica
            };

            return View(model);
        }

        private List<Divisa> ObtenerDivisasSimuladas()
        {
            var rnd = new Random();
            var codigos = new[] { "USD", "EUR", "JPY", "GBP", "CHF", "CAD", "AUD", "CNY", "INR", "BRL" };
            var nombres = new[] { "Dólar", "Euro", "Yen", "Libra", "Franco Suizo", "Dólar Canadiense", "Dólar Australiano", "Yuan", "Rupia", "Real" };
            var divisas = new List<Divisa>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                decimal baseValor = 10 + (decimal)(rnd.NextDouble() * 90); // valor base entre 10 y 100

                divisas.Add(new Divisa
                {
                    Codigo = codigos[i],
                    Nombre = nombres[i],
                    ValorActual = baseValor,
                    ValoresDia = GenerarValoresAleatorios(baseValor, 24, rnd),
                    ValoresSemana = GenerarValoresAleatorios(baseValor, 7, rnd),
                    ValoresMes = GenerarValoresAleatorios(baseValor, 30, rnd),
                    ValoresAnio = GenerarValoresAleatorios(baseValor, 12, rnd)
                });
            }
            return divisas;
        }

        private List<decimal> GenerarValoresAleatorios(decimal baseValor, int cantidad, Random rnd)
        {
            var lista = new List<decimal>();
            decimal valor = baseValor;
            for (int i = 0; i < cantidad; i++)
            {
                // Variación entre -0.5 y +0.5
                valor += (decimal)(rnd.NextDouble() - 0.5);
                if (valor < 1) valor = 1;
                lista.Add(Math.Round(valor, 2));
            }
            return lista;
        }
    }

    public class CasaDeCambioViewModel
    {
        public List<Divisa> Divisas { get; set; }
        public Divisa DivisaSeleccionada { get; set; }
        public string Rango { get; set; } // dia, semana, mes, año
    }
}

