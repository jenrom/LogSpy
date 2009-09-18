using System.Windows;
using LogSpy.UI.Commands;
namespace LogSpy.UI.Views.Dialogs
{
    public class DisplayMessageDialog : IDialog<IDisplayMessageCommand>
    {
        public DisplayMessageDialog(IDisplayMessageCommand dialogHandler)
        {
            DialogHandler = dialogHandler;
            Title = Resources.Dialogs.ErrorDialogTitle;
        }

        public string Title
        {
            get; set;
        }

        public void ShowDialog()
        {
            MessageBox.Show(DialogHandler.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public IDisplayMessageCommand DialogHandler
        {
            get; private set;
        }
    }
}