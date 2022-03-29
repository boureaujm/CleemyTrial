using System;

namespace CleemyCommons.Exceptions
{
    public class DuplicatePaymentException : Exception
    {
        public DuplicatePaymentException()
        {
        }

        public DuplicatePaymentException(string message)
            : base(message)
        {
        }
    }
}