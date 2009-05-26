using System;
using System.Runtime.Serialization;

namespace LogSpy.UI
{
    [Serializable]
    public class MenuException : Exception
    {
        public MenuException()
        {
        }

        public MenuException(string message) : base(message)
        {
        }

        public MenuException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MenuException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}