namespace LogSpy.Core.Model.LogFile
{
    public class FileLogProvider: ILogProvider
    {
        public string FilePath { get; private set; }

        public FileLogProvider(string filePath)
        {
            FilePath = filePath;
        }
    }
}