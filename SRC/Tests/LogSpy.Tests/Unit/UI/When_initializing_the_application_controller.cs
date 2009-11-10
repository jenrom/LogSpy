using LogSpy.UI.PresentationLogic;
using LogSpy.UI.Views;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using LogSpy.UI;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using LogSpy.UI.Commands;
using StructureMap;
using LogSpy.Core.Model;

namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class When_initializing_the_application_controller
    {
        private IShellView shellView;
        private IMenuController menuController;
        private IServiceLocator serviceLocator;

        [SetUp]
        public void before()
        {
            serviceLocator = MockRepository.GenerateStub<IServiceLocator>();
            shellView = MockRepository.GenerateMock<IShellView>();
            menuController = MockRepository.GenerateMock<IMenuController>();
            serviceLocator.Stub(x => x.GetInstance<IMenuController>()).Return(menuController);
        }

        public ApplicationController Sut()
        {
            return new ApplicationController(shellView, serviceLocator);
        }

        [Test]
        public void should_display_the_shell_after_the_initialization_has_been_completed()
        {
            shellView.Expect(x => x.Display());
            Sut().Initialize();
            shellView.VerifyAllExpectations();
        }

        [Test]
        public void should_setup_the_menu_with_default_menu_items()
        {
            menuController.Expect(x => x.RegisterFor<OpenLogFileCommand>(null))
                .Constraints(Is.Matching<MenuItem>(m => m.Name.Equals( MenuItemName.OpenLogFile)));
            Sut().Initialize();
            menuController.VerifyAllExpectations();
        }
    }
}