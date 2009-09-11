using LogSpy.Core.Model;

namespace LogSpy.UI
{
    public interface IApplicationController
    {
        void Register(ILogProvider logProvider);
    }
}