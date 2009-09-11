using System.Collections.ObjectModel;

namespace LogSpy.UI
{
    public interface IMenu
    {
        void Bind(ObservableCollection<MenuItem> menuItems);
    }
}