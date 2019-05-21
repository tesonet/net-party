using System;

namespace NetParty.Utils.Exceptions
{
    public class CredentialsException : Exception
    {
        public CredentialsException() 
            : base("Bad credentials file!")
        {

        }
    }
}
