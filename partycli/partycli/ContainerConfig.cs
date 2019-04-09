﻿using Autofac;
using partycli.core.DataAccess;
using partycli.core.Execution;
using partycli.Commands;
using partycli.Presentation;
using partycli.core.Repositories.Storage;

namespace partycli
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ConsoleOutputParams>().As<IConsoleOutputParams>();
            builder.RegisterType<ConsoleWriter>().As<IConsoleWriter>();
            builder.RegisterType<Executor>().As<IExecutor>();
            builder.RegisterType<ConfigCommandHandler>().As<ICommandHandler<ConfigCommand>>();
            builder.RegisterType<ServerListCommandHandler>().As<ICommandHandler<ServerListCommand>>();
            builder.RegisterType<JsonStorageManager>().As<IStorageManager>();
            builder.RegisterType<ApiClient>().As<IApiClient>();

            return builder.Build();
        }
    }
}
