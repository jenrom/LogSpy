using NUnit.Framework;
using Core;
using NUnit.Framework.SyntaxHelpers;
using LogSpy.UI;

namespace LogSpy.Tests.Acceptance
{
    [TestFixture]
    public class BaseTests
    {
        private Application application;

        [SetUp]
        public void before_each()
        {
            application = Application.Launch(typeof(ShellView).Assembly.Location);
        }

        [TearDown]
        public void after_each()
        {
            application.Kill();
        }

        [Test]
        public void should_start_the_application_with_the_main_window_without_any_problems()
        {
            var window = application.GetWindow(ShellView.WindowName);
            Assert.That(window.Visible, Is.True);
        }
    }
}