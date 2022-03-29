using System;

namespace CleemyCommons.Exceptions
{
    public class UserNotExistException : Exception
    {
        public UserNotExistException()
        {
        }

        public UserNotExistException(string message)
            : base(message)
        {
        }
    }
}