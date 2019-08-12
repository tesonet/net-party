using NUnit.Framework;
using Unity;

namespace partycli.IntegrationTests
{
    [TestFixture]
    class DependencyContainerTests
    {
        [Test]
        public void DependencyContainer_ContainerRegistration_ServiceResolvbable()
        {
            //Setup
            IUnityContainer container = new UnityContainer();

            //Act
            foreach (var registration in container.Registrations)
            {
                //Assert
                Assert.IsNotNull(container.Resolve(registration.RegisteredType, registration.Name));
            }
            container.Dispose();
        }
    }
}
