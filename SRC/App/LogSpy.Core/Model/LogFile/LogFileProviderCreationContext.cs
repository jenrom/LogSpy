namespace LogSpy.Core.Model.LogFile
{
    public class LogFileProviderCreationContext: ProviderCreationContext
    {
        
        public LogFileProviderCreationContext(string fileName)
        {
            FileName = fileName;
        }

        public string FileName{ get; private set; }

        public bool Equals(LogFileProviderCreationContext other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FileName, FileName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LogFileProviderCreationContext)) return false;
            return Equals((LogFileProviderCreationContext) obj);
        }

        public override int GetHashCode()
        {
            return (FileName != null ? FileName.GetHashCode() : 0);
        }
    }
}