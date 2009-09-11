using LogSpy.Core.Model.LogFile;
using LogSpy.UI;
using LogSpy.UI.Commands;
using NUnit.Framework;
using Rhino.Mocks;
using LogSpy.UI.Views.Dialogs;
using LogSpy.Core.Model;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class OpenLogFileCommandTests
    {
        private IDialogLauncher dialogLauncher;
        private ILogFileProviderFactory providerFactory;
        private ILogProvider createdProvider;
        private IApplicationController applicationController;

        [SetUp]
        public void before_each()
        {
            dialogLauncher = MockRepository.GenerateMock<IDialogLauncher>();
            providerFactory = MockRepository.GenerateStub<ILogFileProviderFactory>();
            createdProvider = MockRepository.GenerateStub<ILogProvider>();
            applicationController = MockRepository.GenerateMock<IApplicationController>();
        }


        public OpenLogFileCommand CreateSut()
        {
            return new OpenLogFileCommand(dialogLauncher, providerFactory, applicationController);
        }

        [Test]
        public void should_launch_a_open_log_file_dialog_when_executing_the_command()
        {
            var sut = CreateSut();
            dialogLauncher.Expect(x => x.LaunchFor<IOpenLogFileCommand>(sut));
            sut.Execute(null);
            dialogLauncher.VerifyAllExpectations();
        }

        [Test]
        public void should_create_a_log_file_provider_for_which_a_new_log_source_view_will_be_created_by_the_application_controller()
        {
            var logFileName = "log.txt";
            providerFactory.Stub(x => x.CreateFor(logFileName)).Return(createdProvider);
            applicationController.Expect(x => x.Register(createdProvider));
            CreateSut().OpenLogFileWith(logFileName);
            applicationController.VerifyAllExpectations();
        }
    }
}