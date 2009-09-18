using NUnit.Framework;
using LogSpy.UI.Commands;
using LogSpy.UI.Views.Dialogs;
namespace LogSpy.Tests.Acceptance
{
    [TestFixture]
    public class DisplayMessageTests
    {
        [Test]
        public void should_display_an_error_dialog()
        {
            var command = new DisplayMessageCommand("An error occured");
            var dialog = new DisplayMessageDialog(command);
            dialog.Show();
        }
    }
}