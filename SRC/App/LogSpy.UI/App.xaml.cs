using System.Windows;
using LogSpy.Core.Infrastructure;

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
            Log.AsDebug("The application was initialized");
        }
    }
}
