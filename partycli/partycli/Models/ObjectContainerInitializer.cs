using partycli.Models.Reporsitory;
using partycli.Models.Service;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models
{
    public static class ObjectContainerInitializer
    {
        public static void Init(Container container)
        {
            container.Register<IRepository, Repositories>(Lifestyle.Singleton);
            container.Register<IService, ServiceList>(Lifestyle.Singleton);
            container.Register<IWebService, WebService>(Lifestyle.Singleton);
        }
    }
}
