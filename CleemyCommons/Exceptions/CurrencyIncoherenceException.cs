using System;

namespace CleemyCommons.Exceptions
{
    public class CurrencyIncoherenceException : Exception
    {
        public CurrencyIncoherenceException()
        {

        }

        public CurrencyIncoherenceException(string message)
            : base(message)
        {

        }
    }

}
