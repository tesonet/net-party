﻿using Autofac;
using FluentValidation;
using NetParty.Contracts.Requests.Validators;

namespace NetParty.Application.DI
{
    public static class ServicesContainer
    {
        public static IContainer Container;

        static ServicesContainer()
        {
            CreateContainer();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<LogModule>();
            builder.RegisterModule<HandlersModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<ClientsModule>();
            builder.RegisterModule<RepositoriesModule>();

            builder
                .RegisterAssemblyTypes(typeof(ConfigurationRequestValidator).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}
