using System.Windows;
using LogSpy.UI.Commands;
namespace LogSpy.UI.Views.Dialogs
{
    public class DisplayMessageDialog : IDialog<DisplayMessageCommand>
    {
        public DisplayMessageDialog(DisplayMessageCommand dialogHandler)
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

        public DisplayMessageCommand DialogHandler
        {
            get; private set;
        }
    }
}