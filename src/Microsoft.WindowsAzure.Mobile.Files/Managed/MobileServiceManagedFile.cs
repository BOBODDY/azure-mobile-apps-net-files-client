namespace Microsoft.WindowsAzure.MobileServices.Files.Managed
{
    public class MobileServiceManagedFile : MobileServiceFile
    {
        public static MobileServiceManagedFile FromFile(MobileServiceFile file)
        {
            return new MobileServiceManagedFile
            {
                ContentMD5 = file.ContentMD5,
                Id = file.Id,
                LastModified = file.LastModified,
                Length = file.Length,
                Metadata = file.Metadata,
                Name = file.Name,
                ParentId = file.ParentId,
                StoreUri = file.StoreUri,
                TableName = file.TableName
            };
        }
    }
}
