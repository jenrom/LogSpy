using System;

namespace LogSpy.Core.Model.LogFile
{
    public class LogFileProviderFactory: ILogFileProviderFactory
    {
        public ILogProvider CreateFor(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}