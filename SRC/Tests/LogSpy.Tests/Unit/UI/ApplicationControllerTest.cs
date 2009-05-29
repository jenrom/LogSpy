using LogSpy.UI.Views;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using LogSpy.UI;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using LogSpy.UI.Commands;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class ApplicationControllerTest
    {
        private IShellView shellView;
        private IMenuController menuController;

        [SetUp]
        public void before_each()
        {
            shellView = MockRepository.GenerateMock<IShellView>();
            var serviceLocator = MockRepository.GenerateStub<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(()=> serviceLocator);
            menuController = MockRepository.GenerateMock<IMenuController>();
            serviceLocator.Stub(x => x.GetInstance<IMenuController>()).Return(
                menuController);
        }


        public ApplicationController CreateSut()
        {
            return new ApplicationController(shellView);
        }

        [Test]
        public void the_application_controller_should_display_the_shell_after_the_initialization_has_been_completed()
        {
            shellView.Expect(x => x.Display());
            CreateSut().Initialize();
            shellView.VerifyAllExpectations();
        }

        [Test]
        public void the_application_controller_should_setup_the_menu_with_default_menu_items()
        {
            menuController.Expect(x => x.RegisterFor<IOpenLogFileCommand>(null))
                .Constraints(Is.Matching<MenuItem>(m => m.Name.Equals( MenuItemName.OpenLogFile)));
            CreateSut().Initialize();
            menuController.VerifyAllExpectations();
        }
    }
}