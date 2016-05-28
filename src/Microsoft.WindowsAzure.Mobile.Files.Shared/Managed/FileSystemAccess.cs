// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
#if !WIN_APPS
    public class FileSystemAccess : IFileSystemAccess
    {
        private readonly string basePath;

        public FileSystemAccess(string basePath = "")
        {
            this.basePath = basePath;
        }

        public Task<Stream> CreateAsync(string targetPath)
        {
            return Task.FromResult<Stream>(File.Create(Path.Combine(basePath, targetPath)));
        }

        public Task<Stream> OpenReadAsync(string targetPath)
        {
            return Task.FromResult<Stream>(File.OpenRead(Path.Combine(basePath, targetPath)));
        }

        public Task DeleteAsync(string targetPath)
        {
            File.Delete(Path.Combine(basePath, targetPath));
            return Task.FromResult(true);
        }
    }
#endif
}
