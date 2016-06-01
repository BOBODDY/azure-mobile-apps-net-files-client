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
        public Task<Stream> CreateAsync(string targetPath)
        {
            return Task.FromResult<Stream>(File.Create(targetPath));
        }

        public Task<Stream> OpenReadAsync(string targetPath)
        {
            return Task.FromResult<Stream>(File.OpenRead(targetPath));
        }

        public Task DeleteAsync(string targetPath)
        {
            File.Delete(targetPath);
            return Task.FromResult(true);
        }

        public Task EnsureFolderExistsAsync(string targetPath) 
        {
            if(!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
            return Task.FromResult(true);
        }
    }
#endif
}
