using System;

namespace LogSpy.Core.Model.LogFile
{
    public class LogFileProviderFactory: ILogProviderFactory<LogFileProviderCreationContext>
    {
        public ILogProvider CreateFor(LogFileProviderCreationContext fileName)
        {
            throw new NotImplementedException();
        }
    }
}