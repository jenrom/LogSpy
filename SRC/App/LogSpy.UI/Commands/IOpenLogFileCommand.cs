using System.Windows.Input;
namespace LogSpy.UI.Commands
{
    public interface IOpenLogFileCommand: ICommand
    {
        void OpenLogFileWith(string fileName);
    }
}