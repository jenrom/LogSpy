using System;
using LogSpy.Core.Model;
using LogSpy.UI.Views;
using Microsoft.Practices.Composite.Regions;

namespace LogSpy.UI.PresentationLogic
{
    public class LogSourcePresenter: ILogSourcePresenter, IHandlerOf<LogEntry>
    {
        private readonly ILogProvider logProvider;
        private readonly ILogSourceView logSourceView;
        private IRegion region;

        public LogSourcePresenter(ILogProvider logProvider, ILogSourceView logSourceView, IRegionManager regionManager)
        {
            if (logProvider == null) throw new ArgumentNullException("logProvider");
            if (logSourceView == null) throw new ArgumentNullException("logSourceView");
            if (regionManager == null) throw new ArgumentNullException("region");
            this.logProvider = logProvider;
            this.logSourceView = logSourceView;
            region = regionManager.GetLogSourceViewRegion();
            region.Add(logSourceView);
        }

        public void Activate()
        {
            region.Activate(logSourceView);
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