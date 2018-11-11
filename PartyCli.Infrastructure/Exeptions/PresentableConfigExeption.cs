using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Infrastructure.Exeptions
{
    public class PresentableConfigExeption : PresentableExeption
    {
        public PresentableConfigExeption()
        {
        }

        public PresentableConfigExeption(string message)
            :base($"File \"PartyCli.AppSettings.json\" is corupted. Could not find section: {message}.")
        {            
        }  
   
    }
}
