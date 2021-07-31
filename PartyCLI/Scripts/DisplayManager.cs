using System;
using System.Collections.Generic;

namespace PartyCLI
{
    public class DisplayManager : IDisplayManager
    {
        public void Display(List<Server> sList)
        {
            foreach (Server s in sList)
                Console.WriteLine($"Server: {s.Name} | Distance: {s.Distance}");

            Console.WriteLine($"Servers count: {sList.Count}");
        }
    }
}
