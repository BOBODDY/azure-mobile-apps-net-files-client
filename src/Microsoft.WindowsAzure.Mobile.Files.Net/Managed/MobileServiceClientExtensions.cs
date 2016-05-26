using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Microsoft.WindowsAzure.Mobile.Files.Managed.Ext
{
    public static class MobileServiceClientExtensions
    {
        public static IFileSyncContext InitializeFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store)
        {
            var filesClient = MobileServiceClients.GetFilesClient(client);
            var syncHandler = new LocalStorageSyncHandler(new FileSystemStorageProvider(new FileSystemAccess()), filesClient);
            return client.InitializeFileSyncContext(syncHandler, store);
        }
    }
}
