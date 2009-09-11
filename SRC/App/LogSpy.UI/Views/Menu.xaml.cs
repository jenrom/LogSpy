using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogSpy.UI.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu: UserControl, IMenu
    {

        public Menu()
        {
            InitializeComponent();
        }

        public void Bind(ObservableCollection<MenuItem> menuItems)
        {
            menuItemsList.ItemsSource = menuItems;
        }
    }
}
