using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

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

        public DisplayMessageCommand(IEnumerable<string> messages):this(messages.ToArray())
        {
            
        }

        public DisplayMessageCommand(params string[] messages)
        {
            if (messages == null) throw new ArgumentNullException("messages");
            message = FormatMessage(messages);
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