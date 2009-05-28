using System;
using System.Windows;
using LogSpy.UI.Views.Dialogs;

namespace LogSpy.UI.Commands
{
    public class OpenLogFileCommand: IOpenLogFileCommand
    {
        private readonly IDialogLauncher dialogLauncher;

        public OpenLogFileCommand(IDialogLauncher dialogLauncher)
        {
            if (dialogLauncher == null) throw new ArgumentNullException("dialogLauncher");
            this.dialogLauncher = dialogLauncher;
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
            MessageBox.Show(fileName);
        }
    }
}