namespace LogSpy.UI.Views
{
    public interface IShellView
    {
        void Display();

        IMenu Menu { get; }
    }
}