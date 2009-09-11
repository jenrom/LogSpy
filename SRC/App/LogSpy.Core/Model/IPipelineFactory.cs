namespace LogSpy.Core.Model
{
    public interface IPipelineFactory
    {
        IPipeline CreateWithDefaultSettingsUsing(ILogProvider provider);
    }
}