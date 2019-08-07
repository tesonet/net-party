using Autofac;
using NetParty.CLI.Utils;
using NLog;

namespace NetParty.CLI.DI
{
    public class UtilModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => LogManager.GetCurrentClassLogger()).SingleInstance();
            builder.RegisterType<LoggingInterceptor>();
        }
    }
}