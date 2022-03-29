using System;

namespace CleemyCommons.Exceptions
{
    public class CurrencyNotExistException : Exception
    {
        public CurrencyNotExistException()
        {
        }

        public CurrencyNotExistException(string message)
            : base(message)
        {
        }
    }
}