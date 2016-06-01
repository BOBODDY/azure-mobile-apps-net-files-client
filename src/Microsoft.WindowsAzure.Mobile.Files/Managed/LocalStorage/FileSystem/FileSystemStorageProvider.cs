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
        private readonly string basePath;

        public FileSystemStorageProvider(IFileSystemAccess fileSystem, string basePath = "")
        {
            this.fileSystem = fileSystem;
            this.basePath = basePath;
            fileSystem.EnsureFolderExistsAsync(basePath);
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

        public MobileServiceFile AttachMetadata(MobileServiceFile file)
        {
            return new MobileServiceFileSystemFile(file, GetFilePath(file));
        }

        private string GetFilePath(MobileServiceFile file)
        {
            return fileSystem.GetFullFilePath(Path.Combine(this.basePath, string.Format("{0}-{1}-{2}", file.TableName, file.ParentId, file.Name)));
        }
    }
}
