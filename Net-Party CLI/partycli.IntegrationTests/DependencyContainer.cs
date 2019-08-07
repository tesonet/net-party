using NUnit.Framework;
using Unity;

namespace partycli.IntegrationTests
{
    [TestFixture]
    class DependencyContainerTests
    {

        [Test]
        public void containerRegistrationSuccessfull_serviceResolvbable()
        {
            //Setup
            IUnityContainer container = new UnityContainer();

            //Act
            foreach (var registration in container.Registrations)
            {
                //Assert
                Assert.IsNotNull(container.Resolve(registration.RegisteredType, registration.Name));
            }
        }
    }
}
