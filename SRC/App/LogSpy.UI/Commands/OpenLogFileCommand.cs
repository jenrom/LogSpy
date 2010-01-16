using System;
using LogSpy.Core.Model;
using LogSpy.Core.Model.LogFile;
using LogSpy.UI.Views.Dialogs;
using System.Windows.Input;
using StructureMap;
using Microsoft.Practices.ServiceLocation;

namespace LogSpy.UI.Commands
{
    public class OpenLogFileCommand: ICommand
    {
        private readonly IDialogLauncher dialogLauncher;
        private readonly ILogProviderFactory<FileLogProviderCreationContext> fileProviderFactory;
        private readonly IApplicationController applicationController;

        public OpenLogFileCommand(IDialogLauncher dialogLauncher, 
            ILogProviderFactory<FileLogProviderCreationContext> fileProviderFactory, 
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
            dialogLauncher.LaunchFor(this);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        
        public void OpenLogFileWith(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            var context = new FileLogProviderCreationContext(fileName);
            var provider = fileProviderFactory.CreateFor(context);
            if(context.WasCreated)
            {
                applicationController.Register(provider);   
            }
            else
            {
                var command = new DisplayMessageCommand(context.CreationErrors);
                dialogLauncher.LaunchFor(command);
            }
        }
    }
}