using Microsoft.Practices.ServiceLocation;

namespace LogSpy.UI.Views.Dialogs
{
    public class DialogLauncher: IDialogLauncher
    {
        public void LaunchFor<TDialogHandler>(TDialogHandler handler)
        {
            var dialog = ServiceLocator.Current.GetInstanceWith<IDialog<TDialogHandler>>(handler);
            dialog.ShowDialog();
        }
    }
}