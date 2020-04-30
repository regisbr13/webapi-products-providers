using System;

namespace WebApiProductsProviders.Data.Exceptions
{
    public class IntegrityException : Exception
    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}
