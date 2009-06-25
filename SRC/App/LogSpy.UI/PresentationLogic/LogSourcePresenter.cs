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
        private readonly IPipelineFactory pipelineFactory;
        private IPipeline currentlyUsedPipeline;
        private IRegion region;

        public LogSourcePresenter(ILogProvider logProvider, ILogSourceView logSourceView, IPipelineFactory pipelineFactory, IRegion region)
        {
            if (logProvider == null) throw new ArgumentNullException("logProvider");
            if (logSourceView == null) throw new ArgumentNullException("logSourceView");
            if (pipelineFactory == null) throw new ArgumentNullException("pipelineFactory");
            if (region == null) throw new ArgumentNullException("region");
            this.region = region;
            this.logProvider = logProvider;
            this.logSourceView = logSourceView;
            this.pipelineFactory = pipelineFactory;
            InitializeView();
            InitializePipeline();   
        }

        private void InitializeView()
        {
            region.Add(logSourceView);
        }

        private void InitializePipeline()
        {
            currentlyUsedPipeline = pipelineFactory.CreateWithDefaultSettingsUsing(logProvider);
            currentlyUsedPipeline.SubscribeAs<IHandlerOf<LogEntry>>(this);
        }

        public void Activate()
        {
            region.Activate(logSourceView);
            currentlyUsedPipeline.Start();
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