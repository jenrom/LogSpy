using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace LogSpy.UI
{
    public interface IMenuController
    {
        void Register(MenuItem menuItem);
        MenuItem GetMenuItemWith(MenuItemName name);
        void AttachToMenuItemWith(MenuItemName menuItemName, ICommand command);

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It enables an easier way to attach commands to the menu item")]
        void RegisterFor<TCommand>(MenuItem menuItem) where TCommand : ICommand;
    }
}