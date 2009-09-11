using System;
using log4net;
using log4net.Config;
using Microsoft.Practices.Composite.Logging;
using System.Diagnostics.Contracts;

namespace LogSpy.Core.Infrastructure
{
    /// <summary>
    /// Represents a log4net logger adapter that can be used by Prism
    /// </summary>
    public sealed class Log4NetLogger : ILoggerFacade
    {
        private readonly ILog destinationLog;

        ///// <summary>
        ///// Initializes the log4net logger using app.config
        ///// </summary>
        public Log4NetLogger()
        {
            XmlConfigurator.Configure();
            destinationLog = LogManager.GetLogger(GetType());
            if (destinationLog == null)
            {
                throw new InvalidOperationException("Could not create a logger");
            }
        }


        /// <summary>
        /// Initializes the log4net adapter with a previously created loggers
        /// </summary>
        /// <param name="destinationLog"></param>
        public Log4NetLogger(ILog destinationLog)
        {
            if (destinationLog == null) throw new ArgumentNullException("destinationLog");
            this.destinationLog = destinationLog;
        }

        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry.</param>
        public void Log(string message, Category category, Priority priority)
        {
            Contract.Requires(string.IsNullOrEmpty(message) == false);
            switch(category)
            {
                case Category.Debug:
                    destinationLog.Debug(message);
                    break;
                case Category.Info:
                    destinationLog.Info(message);
                    break;
                case Category.Warn:
                    destinationLog.Warn(message);
                    break;
                case Category.Exception:
                    destinationLog.Error(message);
                    break;
                default:
                    destinationLog.Fatal(message);
                    break;
            }
        }
        
        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(destinationLog != null);
        }

    }
}