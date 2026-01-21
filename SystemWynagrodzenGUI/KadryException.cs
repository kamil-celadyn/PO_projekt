using System;

namespace SystemWynagrodzen
{
    // Punkt 8: Własny wyjątek
    public class KadryException : Exception
    {
        public KadryException(string message) : base(message) { }
    }
}