using Autofac;
using Autofac.Extras.DynamicProxy;
using NetParty.CLI.Utils;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Handlers;

namespace NetParty.CLI.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WhenAuthorize>().As<IRequestHandler<Authorize, AuthorizationResult>>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<WhenGetServerList>().As<IRequestHandler<GetServerList, ServerList>>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<WhenGetLocalServerList>().As<IRequestHandler<GetLocalServerList, ServerList>>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
        }
    }
}
