namespace LogSpy.UI.Views.Dialogs
{
    public interface IDialog
    {
        string Title { get; set; }
        
        void ShowDialog();
    }

    public interface IDialog<TDialogHandler>: IDialog
    {
        TDialogHandler DialogHandler { get; }
    }
}