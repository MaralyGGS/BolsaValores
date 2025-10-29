using System;

namespace BolsaValores.Services
{
    public class ClockService
    {
        // MVP: siempre abierto (puedes parametrizar después)
        public bool IsOpen(DateTimeOffset nowLocal) => true;
    }
}
