using LogSpy.UI.Views;
using NUnit.Framework;
using LogSpy.UI.PresentationLogic;
using Rhino.Mocks;
using LogSpy.Core.Model;
using Microsoft.Practices.Composite.Regions;

namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class LogSourcePresenterTests
    {
        private ILogProvider logProvider;
        private ILogSourceView logSourceView;
        private IRegionManager regionManager;
        private IRegion region;

        public LogSourcePresenter CreateSut()
        {
            return new LogSourcePresenter(logProvider, logSourceView, regionManager);
        }

        [SetUp]
        public void before_each()
        {
            logProvider = MockRepository.GenerateStub<ILogProvider>();
            logSourceView = MockRepository.GenerateStub<ILogSourceView>();
            regionManager = MockRepository.GenerateStub<IRegionManager>();
            region = MockRepository.GenerateMock<IRegion>();
            regionManager.Stub(x => x.Regions[null]).IgnoreArguments().Return(region);
        }

        [Test]
        public void should_indicate_that_the_current_presenter_is_listening_to_the_log_provider()
        {
            Assert.That(CreateSut().IsListeningTo(logProvider));
        }

        [Test]
        public void should_activate_the_view_when_the_presenter_is_activated()
        {
            CreateSut().Activate();
            region.AssertWasCalled(x=>x.Activate(logSourceView));
        }
    }
}