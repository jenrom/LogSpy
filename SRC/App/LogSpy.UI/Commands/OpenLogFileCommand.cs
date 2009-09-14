using System;
using System.Windows.Input;
using LogSpy.Core.Model;
using LogSpy.Core.Model.LogFile;
using LogSpy.UI.Views.Dialogs;

namespace LogSpy.UI.Commands
{
    public interface IOpenLogFileCommand : ICommand
    {
        void OpenLogFileWith(string fileName);
    }

    public class OpenLogFileCommand: IOpenLogFileCommand
    {
        private readonly IDialogLauncher dialogLauncher;
        private readonly ILogProviderFactory<LogFileProviderCreationContext> fileProviderFactory;
        private readonly IApplicationController applicationController;

        public OpenLogFileCommand(IDialogLauncher dialogLauncher, 
            ILogProviderFactory<LogFileProviderCreationContext> fileProviderFactory, 
            IApplicationController applicationController)
        {
            if (dialogLauncher == null) throw new ArgumentNullException("dialogLauncher");
            if (fileProviderFactory == null) throw new ArgumentNullException("fileProviderFactory");
            if (applicationController == null) throw new ArgumentNullException("applicationController");
            this.dialogLauncher = dialogLauncher;
            this.fileProviderFactory = fileProviderFactory;
            this.applicationController = applicationController;
        }

        public void Execute(object parameter)
        {
            dialogLauncher.LaunchFor<IOpenLogFileCommand>(this);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        
        public void OpenLogFileWith(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            var context = new LogFileProviderCreationContext(fileName);
            var provider = fileProviderFactory.CreateFor(context);
            applicationController.Register(provider);
        }
    }
}