using LogSpy.UI;
using LogSpy.UI.PresentationLogic;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;
using LogSpy.UI.Views;
using StructureMap;
using LogSpy.Core.Model;

namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class When_registering_a_new_provider
    {
        private IShellView shellView;
        private ILogSourcePresenter logSourcePresenter;
        private ILogProvider logProvider;
        private StructureMapServiceLocator serviceLocator;

        [SetUp]
        public void before_each()
        {
            serviceLocator = new StructureMapServiceLocator();
            shellView = MockRepository.GenerateStub<IShellView>();
            logSourcePresenter = MockRepository.GenerateMock<ILogSourcePresenter>();
            logProvider = MockRepository.GenerateStub<ILogProvider>();
            ObjectFactory.Initialize(
                x => x.ForRequestedType<ILogSourcePresenter>().TheDefault.IsThis(logSourcePresenter));
        }
        
        public ApplicationController Sut()
        {
            return new ApplicationController(shellView, serviceLocator);
        }

        [Test]
        public void should_activate_the_newly_created_presenter_for_the_log_provider()
        {
            logSourcePresenter.Expect(x => x.Activate());
            Sut().Register(logProvider);
            logSourcePresenter.VerifyAllExpectations();
        }

        [Test]
        public void should_disable_the_previously_activated_presenter()
        {
            //firstRegistration
            var sut = Sut();
            sut.Register(logProvider);
            var newLogProvider = MockRepository.GenerateStub<ILogProvider>();
            var newLogSourcePresenter = MockRepository.GenerateStub<ILogSourcePresenter>();
            ObjectFactory.Initialize(
                x => x.ForRequestedType<ILogSourcePresenter>().TheDefault.IsThis(newLogSourcePresenter));

            logSourcePresenter.Expect(x => x.Close());
            //second registration
            sut.Register(newLogProvider);
            logSourcePresenter.VerifyAllExpectations();
        }

        [Test]
        public void should_not_create_a_new_log_presenter_because_the_new_registered_log_provider_is_equals_to_the_previous_one()
        {
            //firstRegistration
            var sut = Sut();
            sut.Register(logProvider);
            logSourcePresenter.Stub(x => x.IsListeningTo(logProvider)).Return(true);
            logSourcePresenter.Expect(x => x.Close()).Repeat.Never();
            //second registration
            sut.Register(logProvider);
            logSourcePresenter.VerifyAllExpectations();
        }

    }
}