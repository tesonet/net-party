using System;
using Autofac;
using NetParty.Application.DI;
using Serilog;

namespace NetParty.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var scope = ServicesContainer.Container.BeginLifetimeScope())
            {

            }

            Console.ReadLine();
        }
    }
}
