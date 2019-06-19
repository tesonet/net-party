using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetPartyCore.Framework
{
    /**
     *  Simple base controller for handling access to service container
     */
    class CoreController
    {
        private IServiceProvider serviceProvider;

        private void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected T GetSerivce<T>()
        {
            return serviceProvider.GetService<T>();
        }

        internal static T CreateWithProvider<T>(IServiceProvider serviceProvider) where T : CoreController
        {
            var controller = Activator.CreateInstance<T>();
            controller.SetServiceProvider(serviceProvider);
            return controller;
        }
    }
}
