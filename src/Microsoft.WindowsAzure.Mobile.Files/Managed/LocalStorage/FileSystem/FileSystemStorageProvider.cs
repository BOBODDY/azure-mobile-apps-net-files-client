// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public class FileSystemStorageProvider : ILocalStorageProvider
    {
        private readonly IFileSystemAccess fileSystem;

        public FileSystemStorageProvider(IFileSystemAccess fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public async Task AddAsync(MobileServiceFile file, Stream source)
        {
            using (var target = await this.fileSystem.CreateAsync(GetFilePath(file)))
            {
                await source.CopyToAsync(target);
            }
        }

        public async Task DeleteAsync(MobileServiceFile file)
        {
            await this.fileSystem.DeleteAsync(GetFilePath(file));
        }

        public async Task<Stream> GetAsync(MobileServiceFile file)
        {
            return await this.fileSystem.OpenReadAsync(GetFilePath(file));
        }

        public MobileServiceManagedFile AttachMetadata(MobileServiceFile file)
        {
            return new MobileServiceFileSystemFile(file, GetFilePath(file));
        }

        private string GetFilePath(MobileServiceFile file)
        {
            return Path.Combine(string.Format("{0}-{1}-{2}", file.TableName, file.ParentId, file.Name));
        }
    }
}
