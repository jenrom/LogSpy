using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace LogSpy.UI.Views.Dialogs
{
    public class DialogLauncher: IDialogLauncher
    {
        public void LaunchFor<TDialogHandler>(TDialogHandler handler)
        {
            var dialog = ServiceLocator.Current.GetInstanceWith<IDialog<TDialogHandler>, TDialogHandler>(handler);
            dialog.ShowDialog();
        }
    }
}