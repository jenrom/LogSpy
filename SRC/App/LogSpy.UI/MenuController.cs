using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
                    throw new MenuException(string.Format("The menu allready contains a menu item with name {0}",
                                                          menuItem.Name));
                }
                menuItems.Add(menuItem);   
            }
        }

        public MenuItem GetMenuItemWith(MenuItemName name)
        {
            lock (syncObj)
            {
                foreach (var menuItem in menuItems)
                {
                    if(menuItem.Name == name)
                    {
                        return menuItem;
                    }
                }
            }
            throw new MenuException(string.Format("Could not find a registerd menu item with with name {0}", name));
        }

        public void AttachToMenuItemWith(MenuItemName menuItemName, ICommand command)
        {
            GetMenuItemWith(menuItemName).Command = command;
        }
    }
}