namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public class MobileServiceFileSystemFile : MobileServiceManagedFile
    {
        public MobileServiceFileSystemFile(string physicalPath)
        {
            PhysicalPath = physicalPath;
        }

        public string PhysicalPath { get; set; }
    }
}
