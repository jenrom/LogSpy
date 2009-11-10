using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace LogSpy.UI.Views.Dialogs
{
    public class DialogLauncher: IDialogLauncher
    {
        public void LaunchFor<TDialogHandler>(TDialogHandler handler)
        {
            var dialog = ObjectFactory.With(handler).GetInstance<IDialog<TDialogHandler>>();
            //TODO: Need to find why the above code works but the code underneath doesn't works
            //var dialog = ServiceLocator.Current.GetInstanceWith<IDialog<TDialogHandler>>(handler);
            dialog.ShowDialog();
        }
    }
}