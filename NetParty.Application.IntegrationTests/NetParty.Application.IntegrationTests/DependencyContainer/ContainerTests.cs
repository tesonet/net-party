#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace NetParty.Application.IntegrationTests.DependencyContainer
    {
    [TestFixture]
    public class ContainerTests
        {
        [Test]
        public void ContainerIsBuiltSuccessfully_AllServicesAreResolvable()
            {
            // arrange
            var container = DependencyInjection.DependencyContainer.Container;

            // act
            IEnumerable<IServiceWithType> registeredServices =
                container.ComponentRegistry.Registrations.SelectMany(x => x.Services).OfType<IServiceWithType>();
            List<DependencyResolutionException> resolutionExceptions = new List<DependencyResolutionException>();
            foreach (Type serviceType in registeredServices.Select(s => s.ServiceType))
                {
                try
                    {
                    container.TryResolve(serviceType, out _);
                    }
                catch (DependencyResolutionException e)
                    {
                    resolutionExceptions.Add(e);
                    }
                }

            // assert
            string unresolvedServicePrintableList = string.Join("\r\n", resolutionExceptions);
            resolutionExceptions.Count.Should().Be(0, $"all services should be resolvable. Failed to resolve: \r\n{unresolvedServicePrintableList}");
            }

        /// <summary>
        ///     Written specifically to avoid throwing. TryResolve throws a DependencyResolutionException if it fails to resolve
        ///     dependencies for a service.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="serviceType"></param>
        /// <returns>Can service be resolved</returns>
        private static bool CanResolveService(IContainer container, Type serviceType)
            {
            try
                {
                return container.TryResolve(serviceType, out _);
                }
            catch (DependencyResolutionException e)
                {
                return false;
                }
            }
        }
    }
