namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public class MobileServiceFileSystemFile : MobileServiceFile
    {
        public MobileServiceFileSystemFile(MobileServiceFile file, string physicalPath)
        {
            ContentMD5 = file.ContentMD5;
            Id = file.Id;
            LastModified = file.LastModified;
            Length = file.Length;
            Metadata = file.Metadata;
            Name = file.Name;
            ParentId = file.ParentId;
            StoreUri = file.StoreUri;
            TableName = file.TableName;
            
            PhysicalPath = physicalPath;
        }

        public string PhysicalPath { get; set; }
    }
}
