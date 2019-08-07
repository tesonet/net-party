using Autofac;
using NetParty.CLI.ResultsPrinter;

namespace NetParty.CLI.DI
{
    public class ResultsPrinterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleResultsPrinter>().As<IResultsPrinter>();
        }
    }
}