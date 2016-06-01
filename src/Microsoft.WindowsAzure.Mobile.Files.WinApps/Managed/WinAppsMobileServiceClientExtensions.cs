// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.IO;
using Windows.Storage;

namespace Microsoft.WindowsAzure.Mobile.Files.Managed
{
    public static class WinAppsMobileServiceClientExtensions
    {
        public static IFileSyncContext InitializeManagedFileSyncContext(this IMobileServiceClient client, IMobileServiceLocalStore store, string basePath = "")
        {
            if (!string.IsNullOrEmpty(basePath))
                basePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, basePath);
            return client.InitializeManagedFileSyncContext(store, new FileSystemStorageProvider(new WinAppsFileSystemAccess(), basePath));
        }
    }
}
