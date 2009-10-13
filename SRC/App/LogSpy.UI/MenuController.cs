using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;

namespace LogSpy.UI
{
    public class MenuController: IMenuController
    {
        private readonly object syncObj = new object();
        private readonly ObservableCollection<MenuItem> menuItems;


        public MenuController(IMenu menu)
        {
            if (menu == null) throw new ArgumentNullException("menu");
            menuItems = new ObservableCollection<MenuItem>();
            menu.Bind(menuItems);
        }

        public void Register(MenuItem menuItem)
        {
            if (menuItem == null) throw new ArgumentNullException("menuItem");
            lock (syncObj)
            {
                if(menuItems.Contains(menuItem))
                {
                    throw new MenuException(string.Format(Resources.Exceptions.DuplicateMenuItem,menuItem.Name));
                }
                menuItems.Add(menuItem);   
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It enables an easier way to attach commands using the service locator")]
        public void RegisterFor<TCommand>(MenuItem menuItem) where TCommand : ICommand
        {
            menuItem.Command = ServiceLocator.Current.GetInstance<TCommand>();
            Register(menuItem);
        }


        public MenuItem GetMenuItemWith(MenuItemName name)
        {
            lock (syncObj)
            {
                foreach (var menuItem in menuItems)
                {
                    if(menuItem.Name.Equals(name))
                    {
                        return menuItem;
                    }
                }
            }
            throw new MenuException(string.Format(Resources.Exceptions.MenuItemDoesNotExists, name));
        }

        public void AttachToMenuItemWith(MenuItemName menuItemName, ICommand command)
        {
            GetMenuItemWith(menuItemName).Command = command;
        }
    }
}