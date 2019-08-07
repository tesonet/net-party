using System;

namespace NetParty.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string reason, string message) : base(message)
        {
            Reason = reason;
        }

        public string Reason { get; set; }
    }
}