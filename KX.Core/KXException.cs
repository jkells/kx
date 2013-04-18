using System;

namespace KX.Core
{
    public class KXException : Exception
    {
        public KXException(string message) : base(message)
        {
        }
    }
}
