using NUnit.Framework;
using LogSpy.UI;
using Rhino.Mocks;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class ApplicationControllerTest
    {
        private IShellView shellView;

        [SetUp]
        public void before_each()
        {
            shellView = MockRepository.GenerateMock<IShellView>();
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
    }
}