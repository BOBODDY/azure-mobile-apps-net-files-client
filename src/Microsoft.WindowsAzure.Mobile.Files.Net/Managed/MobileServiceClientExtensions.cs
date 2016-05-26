using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Operations;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Files.Sync.Triggers;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Microsoft.WindowsAzure.Mobile.Files.Managed
{
    public static class MobileServiceClientExtensions
    {
        public static IFileSyncContext InitializeManagedFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store)
        {
            var metadataStore = new FileMetadataStore(store);
            var operationQueue = new FileOperationQueue(store);
            var triggerFactory = new DefaultFileSyncTriggerFactory(client, true);
            var filesClient = client.GetFilesClient();
            var syncHandler = new LocalStorageSyncHandler(new FileSystemStorageProvider(new FileSystemAccess()), filesClient);
            var localStorage = new FileSystemStorageProvider(new FileSystemAccess());

            return client.InitializeFileSyncContext(new MobileServiceManagedFileSyncContext(client, metadataStore, operationQueue, triggerFactory, syncHandler, filesClient, localStorage));
        }
    }
}
