using System;
using System.Windows;
using System.Text;

namespace LogSpy.UI.Commands
{
    public interface IDisplayMessageCommand
    {
        string Message { get; }
    }

    public class DisplayMessageCommand: IDisplayMessageCommand
    {
        private readonly string message;
        private readonly string messageSeperator = Environment.NewLine;

        public DisplayMessageCommand(params string[] message)
        {
            if (message == null) throw new ArgumentNullException("message");
            this.message = FormatMessage(message);
        }

        private string FormatMessage(string[] messages)
        {
            var messageBuilder = new StringBuilder();
            for (int i = 0; i < messages.Length; i++)
            {
                if(i != 0)
                {
                    messageBuilder.Append(messageSeperator);
                }
                messageBuilder.Append(messages[i]);
            }
            return messageBuilder.ToString();
        }

        public string Message
        {
            get { return message; }
        }
    }
}