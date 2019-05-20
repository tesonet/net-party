using Autofac;
using Serilog;

namespace NetParty.Application.DI
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                 .Register(c =>
                     new LoggerConfiguration()
                     .MinimumLevel.Debug()
                     .WriteTo.Console()
                     .CreateLogger())
                 .As<ILogger>()
                 .SingleInstance()
                 .PropertiesAutowired();
        }
    }
}
