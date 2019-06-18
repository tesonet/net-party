using Microsoft.Extensions.DependencyInjection;

namespace NetPartyCore.Framework
{
    class CoreController
    {

        private ServiceProvider serviceProvider;

        protected CoreController(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected T GetSerivce<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
