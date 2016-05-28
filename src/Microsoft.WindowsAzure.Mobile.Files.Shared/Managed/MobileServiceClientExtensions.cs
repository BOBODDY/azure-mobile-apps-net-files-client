// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage;
using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Operations;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Files.Sync.Triggers;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.IO;

namespace Microsoft.WindowsAzure.Mobile.Files.Managed
{
    public static class ManagedMobileServiceClientExtensions
    {
#if !WIN_APPS
        public static IFileSyncContext InitializeManagedFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store, string basePath = "")
        {
            // this supports the following values for basePath
            //   empty - current working directory
            //   absolute path - value provided
            //   relative path - relative to application data folder
            if (!string.IsNullOrEmpty(basePath))
                basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), basePath);
            return client.InitializeManagedFileSyncContext(store, new FileSystemAccess(basePath));
        }
#endif

        public static IFileSyncContext InitializeManagedFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store, IFileSystemAccess fileSystem)
        {
            return InitializeManagedFileSyncContext(client, store, new FileSystemStorageProvider(fileSystem));
        }

        public static IFileSyncContext InitializeManagedFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store, ILocalStorageProvider localStorage)
        {
            var metadataStore = new FileMetadataStore(store);
            var operationQueue = new FileOperationQueue(store);
            var triggerFactory = new DefaultFileSyncTriggerFactory(client, true);
            var filesClient = client.GetFilesClient();
            var syncHandler = new LocalStorageSyncHandler(localStorage, filesClient);

            return client.InitializeFileSyncContext(new MobileServiceManagedFileSyncContext(client, metadataStore, operationQueue, triggerFactory, syncHandler, filesClient, localStorage));
        }
    }
}
