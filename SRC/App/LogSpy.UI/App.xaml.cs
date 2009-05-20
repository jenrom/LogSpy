using System.Windows;
using Microsoft.Practices.ServiceLocation;

namespace LogSpy.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new LogSpyBootstrapper();
            bootstrapper.Run();
        }
    }
}
