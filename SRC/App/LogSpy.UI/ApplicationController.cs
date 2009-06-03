using System;
using LogSpy.Core.Model;
using LogSpy.UI.Views;
using Microsoft.Practices.ServiceLocation;
using LogSpy.UI.Commands;
using LogSpy.UI.PresentationLogic;

namespace LogSpy.UI
{
    public class ApplicationController : IApplicationController
    {
        private readonly IShellView shellView;
        private readonly IServiceLocator serviceLocator;
        private IMenuController menuController;
        private ILogSourcePresenter currentLogSourceScreen;

        public ApplicationController(IShellView shellView, IServiceLocator serviceLocator)
        {
            if (shellView == null) throw new ArgumentNullException("shellView");
            if (serviceLocator == null) throw new ArgumentNullException("serviceLocator");
            this.shellView = shellView;
            this.serviceLocator = serviceLocator;
        }

        public IShellView ShellView
        {
            get { return shellView; }
        }

        public void Initialize()
        {
            menuController = serviceLocator.GetInstance<IMenuController>();
            SetupDefaultMenu();
            shellView.Display();
        }

        protected virtual void SetupDefaultMenu()
        {
            menuController.RegisterFor<IOpenLogFileCommand>(new MenuItem("Open File", MenuItemName.OpenLogFile));
        }

        public void Register(ILogProvider logProvider)
        {
            if (logProvider == null) throw new ArgumentNullException("logProvider");
            if(currentLogSourceScreen != null 
                && currentLogSourceScreen.IsListeningTo(logProvider))
            {
                return;
            }
            CloseCurrentLogSourceScreen();
            var presenter = serviceLocator.GetInstanceWith<ILogSourcePresenter>(logProvider);
            Activate(presenter);
        }

        private void Activate(ILogSourcePresenter logSourceScreen)
        {
            currentLogSourceScreen = logSourceScreen;
            logSourceScreen.Activate();
        }

        private void CloseCurrentLogSourceScreen()
        {
            if(currentLogSourceScreen != null)
            {
                currentLogSourceScreen.Close();
            }
        }
    }
}