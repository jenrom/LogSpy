using System;

namespace LogSpy.Core.Model.LogFile
{
    public class FileLogProviderFactory: ILogProviderFactory<FileLogProviderCreationContext>
    {
        public ILogProvider CreateFor(FileLogProviderCreationContext context)
        {
            return new FileLogProvider(context.FileName);
        }
    }
}