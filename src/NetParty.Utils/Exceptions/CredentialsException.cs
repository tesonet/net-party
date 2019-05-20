using System;

namespace NetParty.Application.Exceptions
{
    public class CredentialsException : Exception
    {
        public CredentialsException() 
            : base("Bad credentials file!")
        {

        }
    }
}
