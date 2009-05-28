using LogSpy.UI.Commands;
using NUnit.Framework;
using Rhino.Mocks;
using LogSpy.UI.Views.Dialogs;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class OpenLogFileCommandTests
    {
        private IDialogLauncher dialogLauncher;

        [SetUp]
        public void before_each()
        {
            dialogLauncher = MockRepository.GenerateMock<IDialogLauncher>();
        }


        public OpenLogFileCommand CreateSut()
        {
            return new OpenLogFileCommand(dialogLauncher);
        }

        [Test]
        public void should_launch_a_open_log_file_dialog_when_executing_the_command()
        {
            var sut = CreateSut();
            dialogLauncher.Expect(x => x.LaunchFor<IOpenLogFileCommand>(sut));
            sut.Execute(null);
            dialogLauncher.VerifyAllExpectations();
        }
    }
}