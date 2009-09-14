using System.Diagnostics;
using NUnit.Framework;
using LogSpy.UI.Commands;
using NUnit.Framework.SyntaxHelpers;
using System;
namespace LogSpy.Tests.Unit.UI
{
    [TestFixture]
    public class DisplayMessageCommandTests
    {
        [Test]
        public void initializing_the_command_with_one_messasge_should_instantiate_the_command_with_the_given_message()
        {
            var expectedMessage = "Some message";
            var command = new DisplayMessageCommand(expectedMessage);
            Assert.That(command.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void initializing_the_command_with_more_messages_should_instantiate_the_command_with_the_given_messages_seperated_with_a_new_line()
        {
            var command = new DisplayMessageCommand("Some message", "Another message");
            Assert.That(command.Message, Is.EqualTo("Some message" + Environment.NewLine + "Another message"));
        }
    }
}