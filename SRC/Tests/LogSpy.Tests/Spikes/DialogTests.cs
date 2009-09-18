using NUnit.Framework;
using System.Windows;
using NUnit.Framework.SyntaxHelpers;

namespace LogSpy.Tests.Spikes
{
    [TestFixture]
    [Ignore("This is a spike solution")]
    public class DialogTests
    {
        [Test]
        public void should_display_an_error_message_box()
        {
            MessageBoxResult result = MessageBox.Show("Message", "Caption", MessageBoxButton.OK, MessageBoxImage.Error);
            Assert.That(result, Is.EqualTo(MessageBoxResult.OK));
        }
    }
}