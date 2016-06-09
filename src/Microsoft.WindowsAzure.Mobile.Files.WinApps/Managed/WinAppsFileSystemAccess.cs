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
        private readonly StorageFolder folder;

        public WinAppsFileSystemAccess(StorageFolder folder)
        {
            this.folder = folder;
        }

        public async Task<Stream> CreateAsync(string targetPath)
        {
            var file = await folder.CreateFileAsync(targetPath);
            return await file.OpenStreamForWriteAsync();
        }

        public async Task<Stream> OpenReadAsync(string targetPath)
        {
            return await folder.OpenStreamForReadAsync(targetPath);
        }

        public async Task DeleteAsync(string targetPath)
        {
            var file = await folder.GetFileAsync(targetPath);
            await file.DeleteAsync();
        }

        public async Task EnsureFolderExistsAsync(string targetPath)
        {
            // the folder we were passed the constructor must already exist
        }

        public string GetFullFilePath(string targetPath)
        {
            // this is not entirely correct. the only way to correctly retrieve the
            // full path is to make this method async and call folder.GetFileAsync
            return targetPath;
        }
    }
}
