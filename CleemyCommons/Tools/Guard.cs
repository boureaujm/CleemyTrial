using System;

namespace CleemyCommons.Tools
{
    public static class Guard
    {
        public static void IsNotNull(object value, string message) {
            if (value == null)
                throw new ArgumentNullException(message);
        }

        public static void IsNotZeroNegative(object value, string message)
        {
            if (value is int && ((int)value < 0 || (int)value == 0))
                throw new ArgumentException(message);
        }
    }
}
