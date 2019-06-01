#region Using

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
            var container = Application.DependencyContainer.Container;

            // act
            IEnumerable<IServiceWithType> registeredServices =
                container.ComponentRegistry.Registrations.SelectMany(x => x.Services).OfType<IServiceWithType>();
            IEnumerable<IServiceWithType> unresolvableServices = registeredServices.Where(s => !container.TryResolve(s.ServiceType, out _));

            // assert
            string unresolvedServicePrintableList = string.Join("; ", unresolvableServices.Select(s => s.ServiceType.Name));
            unresolvableServices.Count().Should().Be(0, $"all services should be resolvable. Failed to resolve: {unresolvedServicePrintableList}");
            }
        }
    }
