using System;
using LogSpy.Core.Model;
using LogSpy.UI.Views;

namespace LogSpy.UI.PresentationLogic
{
    public class LogSourcePresenter: ILogSourcePresenter
    {
        private readonly ILogProvider logProvider;
        private readonly ILogSourceView logSourceView;

        public LogSourcePresenter(ILogProvider logProvider, ILogSourceView logSourceView)
        {
            if (logProvider == null) throw new ArgumentNullException("logProvider");
            if (logSourceView == null) throw new ArgumentNullException("logSourceView");
            this.logProvider = logProvider;
            this.logSourceView = logSourceView;
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public bool IsListeningTo(ILogProvider provider)
        {
            return logProvider.Equals(provider);
        }
    }
}