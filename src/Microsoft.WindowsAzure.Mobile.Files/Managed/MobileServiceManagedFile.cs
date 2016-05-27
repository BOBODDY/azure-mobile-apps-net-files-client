// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed
{
    // this is purely to avoid consumers of the managed implementation having to import the 
    // Files namespace as that would introduce collisions on the MobileServiceSyncTable<T> 
    // extension methods
    public class MobileServiceManagedFile : MobileServiceFile
    {
        public static MobileServiceManagedFile FromMobileServiceFile(MobileServiceFile file)
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
