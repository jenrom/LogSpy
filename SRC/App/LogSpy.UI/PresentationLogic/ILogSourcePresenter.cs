
using LogSpy.Core.Model;

namespace LogSpy.UI.PresentationLogic
{
    public interface ILogSourcePresenter: IScreen
    {
        bool IsListeningTo(ILogProvider provider);
    }
}