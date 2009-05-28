using System;
using LogSpy.UI.Views;
using Microsoft.Practices.ServiceLocation;
using LogSpy.UI.Commands;

namespace LogSpy.UI
{
    public class ApplicationController
    {
        private readonly IShellView shellView;
        private IMenuController menuController;

        public ApplicationController(IShellView shellView)
        {
            if (shellView == null) throw new ArgumentNullException("shellView");
            this.shellView = shellView;
        }

        public IShellView ShellView
        {
            get { return shellView; }
        }

        public void Initialize()
        {
            menuController = ServiceLocator.Current.GetInstance<IMenuController>();
            SetupDefaultMenu();
            shellView.Display();
        }

        protected  virtual void SetupDefaultMenu()
        {
            menuController.RegisterFor<IOpenLogFileCommand>(new MenuItem("Open File", MenuItemName.OpenLogFile));
        }
    }
}