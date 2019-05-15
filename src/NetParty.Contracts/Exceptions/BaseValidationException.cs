using System;

namespace NetParty.Contracts.Exceptions
{
    public class BaseValidationException : Exception
    {
        public BaseValidationException(string message) : base(message)
        {
        }
    }
}