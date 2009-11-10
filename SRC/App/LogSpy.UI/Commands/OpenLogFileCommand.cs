using System;
using LogSpy.Core.Model;
using LogSpy.Core.Model.LogFile;
using LogSpy.UI.Views.Dialogs;
using System.Windows.Input;
using StructureMap;

namespace LogSpy.UI.Commands
{
    public class OpenLogFileCommand: ICommand
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
            var context = new LogFileProviderCreationContext(fileName);
            var provider = fileProviderFactory.CreateFor(context);
            if(false == context.WasCreated)
            {
                
                var command = new DisplayMessageCommand(context.CreationErrors);
                var instance = ObjectFactory.With(command).GetInstance<IDialog<DisplayMessageCommand>>();
                dialogLauncher.LaunchFor(command);
            }
            else
            {
                applicationController.Register(provider);   
            }
        }
    }
}