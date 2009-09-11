using System.Collections.ObjectModel;
using LogSpy.UI;
using NUnit.Framework;
using Rhino.Mocks;
using System.Windows.Input;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Specialized;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class MenuControllerTests
    {
        private IMenu menu;
        private MenuItem openLogFileNameMenuItem;
        private MenuItem stopListeningMenuItem;

        [SetUp]
        public void before_each()
        {
            menu = MockRepository.GenerateMock<IMenu>();
            openLogFileNameMenuItem = new MenuItem("Open File", MenuItemName.OpenLogFile);
            stopListeningMenuItem = new MenuItem("Stop Listening", MenuItemName.StopListening);
        }

        [Test]
        public void Should_be_able_to_get_a_previously_added_menu_item_by_his_name_()
        {
            var sut = CreateSUT();
            sut.Register(openLogFileNameMenuItem);
            sut.Register(stopListeningMenuItem);
            var retrievedMenuItem = sut.GetMenuItemWith(MenuItemName.OpenLogFile);
            Assert.That(retrievedMenuItem, Is.EqualTo(openLogFileNameMenuItem));
        }

        [Test]
        [ExpectedException(typeof(MenuException))]
        public void should_not_be_able_to_register_the_same_menu_item_twice()
        {
            var sut = CreateSUT();
            sut.Register(openLogFileNameMenuItem);
            sut.Register(openLogFileNameMenuItem);

        }

        [Test]
        public void should_be_able_to_attach_a_command_to_a_menu_item_by_the_name()
        {
            var sut = CreateSUT();
            sut.Register(openLogFileNameMenuItem);
            var command = MockRepository.GenerateStub<ICommand>();
            sut.AttachToMenuItemWith(MenuItemName.OpenLogFile,command);
            var menuItem = sut.GetMenuItemWith(MenuItemName.OpenLogFile);
            Assert.That(menuItem.Command, Is.EqualTo(command));
        }

        [Test]
        public void should_bind_a_observable_collection_to_the_menu_when_creating_the_controller_that_will_notify_whenever_a_change_occurs()
        {
            var constraint = new GenericConstraint<ObservableCollection<MenuItem>>();
            menu.Expect(x => x.Bind(null)).Constraints(constraint);
            CreateSUT();
            var menuItems = constraint.GetParameter();
            Assert.That(menuItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void should_notify_the_menu_whenever_a_new_menu_item_has_been_added()
        {
            var constraint = new GenericConstraint<ObservableCollection<MenuItem>>();
            menu.Expect(x=>x.Bind(null)).Constraints(constraint);
            var sut = CreateSUT();
            var menuItems = constraint.GetParameter();
            bool aMenuItemWasAdded = false;
            menuItems.CollectionChanged += (sender, args) =>
                                               {
                                                   aMenuItemWasAdded = true;
                                                   Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
                                               };
            sut.Register(openLogFileNameMenuItem);
            Assert.That(aMenuItemWasAdded);
        }


        private MenuController CreateSUT()
        {
            return new MenuController(menu);
        }
    }
}