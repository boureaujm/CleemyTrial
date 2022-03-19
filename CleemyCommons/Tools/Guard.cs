using System;

namespace CleemyCommons.Tools
{
    public static class Guard
    {
        public static void IsNotNull(object value, string message) {
            if (value == null)
                throw new ArgumentNullException(message);
        }
    }
}
