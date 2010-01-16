using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Microsoft.Practices.ServiceLocation;
using LogSpy.UI;
using System;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class StructureMapServiceLocatorTests
    {

        [Test]
        public void shold_retrieve_a_component_without_arguments()
        {
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            var tester = ServiceLocator.Current.GetInstance<ServiceLocatorTester>();
            Assert.That(tester, Is.Not.Null);
        }

        [Test]
        public void shold_retrieve_an_instance_with_specified_argument()
        {
            ServiceLocator.SetLocatorProvider(()=>new StructureMapServiceLocator());
            var argument = "test argument";
            var tester = ServiceLocator.Current.GetInstanceWith<ServiceLocatorTester, object>(argument);
            Assert.That(tester.RequiredArgument, Is.EqualTo(argument));
        }

        [Test]
        public void should_retrieve_an_instance_with_a_newly_created_instance_because_the_types_are_not_matching()
        {
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            var argument = "test argument";
            var tester = ServiceLocator.Current.GetInstanceWith<ServiceLocatorTester, string>(argument);
            Assert.That(tester.RequiredArgument, Is.Not.EqualTo(argument));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_not_allow_to_use_the_structure_map_extensions_in_case_if_the_underlying_service_locator_is_not_based_on_structure_map()
        {
            ServiceLocator.SetLocatorProvider(() => null);
            ServiceLocator.Current.GetInstanceWith<ServiceLocatorTester, string>(null);
        }

        public class ServiceLocatorTester
        {
            private readonly object requiredArgument;

            public ServiceLocatorTester()
            {
                
            }

            public ServiceLocatorTester(object requiredArgument)
            {
                this.requiredArgument = requiredArgument;
            }

            public object RequiredArgument
            {
                get { return requiredArgument; }
            }
        }
    }
}