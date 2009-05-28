using System.Windows.Input;

namespace LogSpy.UI
{
    public interface IMenuController
    {
        void Register(MenuItem menuItem);
        MenuItem GetMenuItemWith(MenuItemName name);
        void AttachToMenuItemWith(MenuItemName menuItemName, ICommand command);
        void RegisterFor<TCommand>(MenuItem menuItem) where TCommand : ICommand;
    }
}