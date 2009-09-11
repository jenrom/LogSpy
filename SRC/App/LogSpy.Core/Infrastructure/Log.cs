using System;
using Microsoft.Practices.Composite.Logging;
namespace LogSpy.Core.Infrastructure
{
    /// <summary>
    /// Represents a log static gateway
    /// </summary>
    public static class Log
    {
        private static ILoggerFacade logger = new TraceLogger();

        /// <summary>
        /// Sets the logger that will be used internaly by the log gateway
        /// </summary>
        /// <param name="loggerFacade"></param>
        public static void Use(ILoggerFacade loggerFacade)
        {
            if (loggerFacade == null) throw new ArgumentNullException("loggerFacade");
            logger = loggerFacade;
        }

        /// <summary>
        /// Resets the log gateway to the default logger
        /// <remarks>The default logger is a TraceLogger</remarks>
        /// </summary>
        public static void Reset()
        {
            logger = new TraceLogger();   
        }

        /// <summary>
        /// Logs a message as a debug message
        /// </summary>
        /// <param name="message">The message that will be loged</param>
        public static void AsDebug(object message)
        {
            if (message == null) throw new ArgumentNullException("message");
            logger.Log(message.ToString(), Category.Debug, Priority.None);
        }

        /// <summary>
        /// Logs a message as an info message
        /// </summary>
        /// <param name="message">The message that will be loged</param>
        public static void AsInfo(object message)
        {
            if (message == null) throw new ArgumentNullException("message");
            logger.Log(message.ToString(), Category.Info, Priority.None);
        }

        /// <summary>
        /// Logs a message as a warning message
        /// </summary>
        /// <param name="message">The message that will be loged</param>
        public static void AsWarning(object message)
        {
            if (message == null) throw new ArgumentNullException("message");
            logger.Log(message.ToString(), Category.Warn, Priority.None);
        }

        /// <summary>
        /// Logs a message as an error message
        /// </summary>
        /// <remarks>If the message is an object of type exeception the stacktrace of this exeception will be also logged</remarks>
        /// <param name="message">The message that will be loged</param>
        public static void Error(object message)
        {
            if (message == null) throw new ArgumentNullException("message");
                logger.Log(message.ToString(), Category.Exception, Priority.None);    
        }
    }
}