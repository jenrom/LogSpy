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
        public void shold_retrieve_the_tested_component_with_specified_argument()
        {
            ServiceLocator.SetLocatorProvider(()=>new StructureMapServiceLocator());
            var argument = "test argument";
            var tester = ServiceLocator.Current.GetInstanceWith<ServiceLocatorTester>(argument);
            Assert.That(tester.RequiredArgument, Is.EqualTo(argument));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_not_allow_to_use_the_structure_map_extensions_in_case_if_the_underlying_service_locator_is_not_based_on_structure_map()
        {
            ServiceLocator.SetLocatorProvider(() => null);
            ServiceLocator.Current.GetInstanceWith<ServiceLocatorTester>(null);
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