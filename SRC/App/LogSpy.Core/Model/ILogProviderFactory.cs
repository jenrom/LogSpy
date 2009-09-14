namespace LogSpy.Core.Model
{
    public interface ILogProviderFactory<T> where T: ProviderCreationContext
    {
        ILogProvider CreateFor(T context);
    }
}