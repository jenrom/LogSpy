namespace LogSpy.Core.Model
{
    public interface IPipeline
    {
        void SubscribeAs<T>(T observer);
        void Start();
    }
}