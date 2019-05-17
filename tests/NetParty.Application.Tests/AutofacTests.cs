using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using NetParty.Application.DI;
using NUnit.Framework;

namespace NetParty.Application.Tests
{
        [TestFixture]
        public class AutofacTest
        {
            [Test]
            public void TestAutofacRegistrations()
            {
                IContainer container = ServicesContainer.Container;

                IEnumerable<IServiceWithType> autofacServices = container.ComponentRegistry.Registrations.SelectMany(x => x.Services).OfType<IServiceWithType>();
                foreach (IServiceWithType svc in autofacServices)
                {
                    Console.WriteLine("Resolving registration: '{0}'", svc);

                    IServiceWithType serviceWithType = svc;

                    Assert.DoesNotThrow(() =>
                    {
                        object resolvedService;

                        KeyedService keyedService = serviceWithType as KeyedService;
                        if (keyedService != null)
                        {
                            resolvedService = container.ResolveKeyed(keyedService.ServiceKey, serviceWithType.ServiceType);
                        }
                        else
                        {
                            resolvedService = container.Resolve(serviceWithType.ServiceType);
                        }

                        if (resolvedService == null)
                            throw new Exception("Autofac service not found: " + serviceWithType.ServiceType.Name);
                    },
                    
                    String.Format("Failed to resolve autofac service '{0}' or one of it's dependencies", serviceWithType)
                    );
                }
            }
        }
}
