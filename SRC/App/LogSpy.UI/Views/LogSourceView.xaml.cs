using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogSpy.UI.Views
{
    /// <summary>
    /// Interaction logic for LogSourceView.xaml
    /// </summary>
    public partial class LogSourceView : UserControl, ILogSourceView
    {
        public LogSourceView()
        {
            InitializeComponent();
        }
    }
}
