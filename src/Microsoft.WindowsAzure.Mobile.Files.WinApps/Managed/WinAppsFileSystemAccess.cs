// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public class WinAppsFileSystemAccess : IFileSystemAccess
    {
        public async Task<Stream> CreateAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(targetPath);
            return await file.OpenStreamForWriteAsync();
        }

        public async Task<Stream> OpenReadAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(targetPath);
            return await file.OpenStreamForReadAsync();
        }

        public async Task DeleteAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(targetPath);
            await file.DeleteAsync();
        }

        public async Task EnsureFolderExistsAsync(string targetPath)
        {
            // this just ensures the folder exists for now, doesn't create
            await StorageFolder.GetFolderFromPathAsync(targetPath);
        }
    }
}
