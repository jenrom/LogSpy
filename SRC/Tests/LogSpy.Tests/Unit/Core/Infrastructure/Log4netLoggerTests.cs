using LogSpy.Core.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;
using log4net;
using Microsoft.Practices.Composite.Logging;

namespace LogSpy.Tests.Unit.Core.Infrastructure
{
    [TestFixture]
    public class Log4NetLoggerTests
    {
        private ILog log;

        [SetUp]
        public void before_each()
        {
            log = MockRepository.GenerateMock<ILog>();
        }


        public Log4NetLogger CreateSut()
        {
            return new Log4NetLogger(log);
        }

        [Test]
        public void should_forward_a_debug_message_to_the_destination_logger()
        {
            var message = "test";
            log.Expect(x => x.Debug(message));
            CreateSut().Log(message, Category.Debug, Priority.None);
            log.VerifyAllExpectations();
        }


        [Test]
        public void should_forward_an_info_message_to_the_destination_logger()
        {
            var message = "test";
            log.Expect(x => x.Info(message));
            CreateSut().Log(message, Category.Info, Priority.None);
            log.VerifyAllExpectations();
        }

        [Test]
        public void should_forward_a_warning_message_to_the_destination_logger()
        {
            var message = "test";
            log.Expect(x => x.Warn(message));
            CreateSut().Log(message, Category.Warn, Priority.None);
            log.VerifyAllExpectations();
        }

        [Test]
        public void should_forward_an_error_message_to_the_destination_logger()
        {
            var message = "test";
            log.Expect(x => x.Error(message));
            CreateSut().Log(message, Category.Exception, Priority.None);
            log.VerifyAllExpectations();
        }
    }
}