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
        private ILogProviderFactory<LogFileProviderCreationContext> providerFactory;
        private ILogProvider createdProvider;
        private IApplicationController applicationController;
        private LogFileProviderCreationContext context;

        [SetUp]
        public void before_each()
        {
            dialogLauncher = MockRepository.GenerateMock<IDialogLauncher>();
            providerFactory = MockRepository.GenerateStub<ILogProviderFactory<LogFileProviderCreationContext>>();
            createdProvider = MockRepository.GenerateStub<ILogProvider>();
            applicationController = MockRepository.GenerateMock<IApplicationController>();
            context = new LogFileProviderCreationContext("log.txt");
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
        public void should_not_register_the_provider_because_the_factory_was_not_able_to_create_a_provider()
        {
            providerFactory.Stub(x => x.CreateFor(context)).Return(null).WhenCalled(x=>context.WasCreated = false);
        }

        [Test]
        public void should_create_a_log_file_provider_for_which_a_new_log_source_view_will_be_created_by_the_application_controller()
        {
            providerFactory.Stub(x => x.CreateFor(context)).Return(createdProvider);
            applicationController.Expect(x => x.Register(createdProvider));
            CreateSut().OpenLogFileWith(context.FileName);
            applicationController.VerifyAllExpectations();
        }
    }
}