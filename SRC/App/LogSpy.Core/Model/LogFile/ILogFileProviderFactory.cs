namespace LogSpy.Core.Model.LogFile
{
    public interface ILogFileProviderFactory
    {
        ILogProvider CreateFor(string fileName);
    }
}