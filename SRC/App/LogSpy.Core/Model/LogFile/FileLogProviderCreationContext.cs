namespace LogSpy.Core.Model.LogFile
{
    public class FileLogProviderCreationContext: ProviderCreationContext
    {
        
        public FileLogProviderCreationContext(string fileName)
        {
            FileName = fileName;
        }

        public string FileName{ get; private set; }

        public bool Equals(FileLogProviderCreationContext other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FileName, FileName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FileLogProviderCreationContext)) return false;
            return Equals((FileLogProviderCreationContext) obj);
        }

        public override int GetHashCode()
        {
            return (FileName != null ? FileName.GetHashCode() : 0);
        }
    }
}