using System;
using System.Windows.Input;
namespace LogSpy.UI.Commands
{
    public class OpenLogFileCommand: ICommand
    {
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}