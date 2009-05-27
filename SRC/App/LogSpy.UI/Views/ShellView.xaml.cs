using System;
using System.Resources;

namespace LogSpy.UI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public sealed partial class ShellView: IShellView
    {
        public const string WindowName = "LogSpy";

        public ShellView()
        {
            InitializeComponent();
        }

        public void Display()
        {
            Show();
        }

        public IMenu Menu
        {
            get { return menu; }
        }
    }
}
