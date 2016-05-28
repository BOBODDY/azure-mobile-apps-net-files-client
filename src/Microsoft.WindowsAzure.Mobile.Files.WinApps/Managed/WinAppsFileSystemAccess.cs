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
        private readonly string basePath;

        public WinAppsFileSystemAccess(string basePath = "")
        {
            this.basePath = basePath;
        }

        public async Task<Stream> CreateAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(basePath, targetPath));
            return await file.OpenStreamForWriteAsync();
        }

        public async Task<Stream> OpenReadAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(basePath, targetPath));
            return await file.OpenStreamForReadAsync();
        }

        public async Task DeleteAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(basePath, targetPath));
            await file.DeleteAsync();
        }
    }
}
