namespace LogSpy.UI.Views.Dialogs
{
    public interface IDialogLauncher
    {
        void LaunchFor<TDialogHandler>(TDialogHandler command);
    }
}