using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Infrastructure.Exeptions
{
    public class PresentableExeption : Exception
    {
        public PresentableExeption()
        {
        }

        public PresentableExeption(string message)
            : base(message)
        {
        }

        public PresentableExeption(string message, Exception inner)
            : base(message, inner)
        {
        }
   
    }
}
