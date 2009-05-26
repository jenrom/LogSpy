using System.Windows.Input;
using NUnit.Framework;
using LogSpy.UI;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class MenuItemTests
    {
        [Test]
        public void When_changing_the_command_a_property_change_event_should_be_raised()
        {
            var menuItem = new MenuItem("Menu Item", new MenuItemName("sciema"));
            bool wasRaised = false;
            menuItem.PropertyChanged += (source, args) =>
                                            {
                                                wasRaised = true;
                                                Assert.That(args.PropertyName, Is.EqualTo("Command"));
                                            };
            menuItem.Command = MockRepository.GenerateStub<ICommand>();
            Assert.That(wasRaised);
            
        }
    }
}