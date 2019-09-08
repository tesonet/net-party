using System;

namespace partycli.Contracts.Exceptions
{
    public class PartyException: Exception
    {
        public PartyException(string message): base(message)
        {
            
        }
    }
}
