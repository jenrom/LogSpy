using LogSpy.UI.Views;
using NUnit.Framework;
using LogSpy.UI.PresentationLogic;
using Rhino.Mocks;
using LogSpy.Core.Model;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class LogSourcePresenterTests
    {
        private ILogProvider logProvider;
        private ILogSourceView logSourceView;

        public LogSourcePresenter CreateSut()
        {
            return new LogSourcePresenter(logProvider, logSourceView);
        }

        [SetUp]
        public void before_each()
        {
            logProvider = MockRepository.GenerateStub<ILogProvider>();
            logSourceView = MockRepository.GenerateMock<ILogSourceView>();
        }


        [Test]
        public void should_indicate_that_the_current_presenter_is_listening_to_the_log_provider()
        {
            Assert.That(CreateSut().IsListeningTo(logProvider));
        }
    }
}