#region Using

using Autofac;
using Serilog;

#endregion

namespace NetParty.Application.DependencyInjection.Modules
    {
    public class LoggerModule : Module
        {
        protected override void Load(ContainerBuilder builder)
            {
            builder.RegisterInstance<ILogger>(new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger());
            }
        }
    }
