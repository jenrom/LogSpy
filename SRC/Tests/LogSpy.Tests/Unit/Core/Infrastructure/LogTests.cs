using System;
using NUnit.Framework;
using LogSpy.Core.Infrastructure;
using Microsoft.Practices.Composite.Logging;
using Rhino.Mocks;

namespace LogSpy.Tests.Unit.Core.Infrastructure
{
    [TestFixture]
    public class LogTests
    {
        private ILoggerFacade logger;

        [SetUp]
        public void before_each()
        {
            logger = MockRepository.GenerateMock<ILoggerFacade>();
        }

        [TearDown]
        public void after_each()
        {
            Log.Reset();
        }

        [Test]
        public void should_not_fail_event_if_the_log_was_not_set_becuase_a_default_log_is_used()
        {
            Log.AsDebug("test");
        }

        [Test]
        public void should_use_the_log_that_was_setup_and_log_a_debug_message()
        {
            Log.Use(logger);
            var message = "test";
            logger.Expect(x=>x.Log(message, Category.Debug, Priority.None));
            Log.AsDebug(message);
            logger.VerifyAllExpectations();
        }

        [Test]
        public void should_use_the_log_that_was_setup_and_log_an_info_message()
        {
            Log.Use(logger);
            var message = "test";
            logger.Expect(x => x.Log(message, Category.Info, Priority.None));
            Log.AsInfo(message);
            logger.VerifyAllExpectations();
        }

        [Test]
        public void should_use_the_log_that_was_setup_and_log_a_warning_message()
        {
            Log.Use(logger);
            var message = "test";
            logger.Expect(x => x.Log(message, Category.Warn, Priority.None));
            Log.AsWarning(message);
            logger.VerifyAllExpectations();
        }


        [Test]
        public void should_use_the_log_that_was_setup_and_log_an_exception_with_stack_trace()
        {
            Log.Use(logger);
            try
            {
                throw new Exception("This is a basic exception with stack trace");
            }
            catch(Exception ex)
            {
                logger.Expect(x => x.Log(ex.ToString(), Category.Exception, Priority.None));
                Log.Error(ex);
                logger.VerifyAllExpectations();
            }
        }

        [Test]
        public void should_use_the_log_that_was_setup_and_log_an_error_message()
        {
            Log.Use(logger);
            var test = "Test";
            logger.Expect(x => x.Log(test, Category.Exception, Priority.None));
            Log.Error(test);
            logger.VerifyAllExpectations();
        }

    }
}