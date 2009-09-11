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
        private IPipelineFactory pipelineFactory;
        private IRegion region;
        private IPipeline pipeline;

        public LogSourcePresenter CreateSut()
        {
            return new LogSourcePresenter(logProvider, logSourceView, pipelineFactory, region);
        }

        [SetUp]
        public void before_each()
        {
            logProvider = MockRepository.GenerateStub<ILogProvider>();
            logSourceView = MockRepository.GenerateMock<ILogSourceView>();
            pipelineFactory = MockRepository.GenerateMock<IPipelineFactory>();
            pipeline = MockRepository.GenerateMock<IPipeline>();
            pipelineFactory.Stub(x=>x.CreateWithDefaultSettingsUsing(logProvider)).Return(pipeline);
            region = MockRepository.GenerateMock<IRegion>();
        }


        [Test]
        public void should_indicate_that_the_current_presenter_is_listening_to_the_log_provider()
        {
            Assert.That(CreateSut().IsListeningTo(logProvider));
        }

        [Test]
        public void should_subscribe_to_the_newly_created_pipeline_so_that_the_log_entries_will_be_published_to_the_presenter()
        {
            var presenter = CreateSut();
            pipeline.AssertWasCalled(x=>x.SubscribeAs<IHandlerOf<LogEntry>>(presenter));
        }

        [Test]
        public void when_activating_the_screen_should_wake_up_the_pipeline_so_that_the_log_entries_will_be_recieved()
        {
            CreateSut().Activate();
            pipeline.AssertWasCalled(x=>x.Start());
        }

        [Test]
        public void when_creating_the_presenter_should_attach_the_view_to_the_region()
        {
            CreateSut();
            region.AssertWasCalled(x=>x.Add(logSourceView));
        }


        [Test]
        public void should_activate_the_view_when_the_presenter_is_activated()
        {
            CreateSut().Activate();
            region.AssertWasCalled(x=>x.Activate(logSourceView));
        }
    }
}