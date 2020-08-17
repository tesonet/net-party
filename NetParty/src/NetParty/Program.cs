using System;
using LightInject;
using NetParty.Application;
using NetParty.Application.Commands;

namespace NetParty
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using(var container = Container.Build())
            {
                var app = CommandBuilder.Build(container.GetAllInstances<ICommand>());

                app.Execute(args);
            }

#if (DEBUG) 
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
#endif
        }
    }
}
