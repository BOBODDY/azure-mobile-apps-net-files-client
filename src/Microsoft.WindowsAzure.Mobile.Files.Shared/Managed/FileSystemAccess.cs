using System;
using System.IO;
using System.Threading.Tasks;
#if WIN_APPS
using Windows.Storage;
#endif

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public class FileSystemAccess : IFileSystemAccess
    {
#if __IOS__ || __UNIFIED__ || __ANDROID__ || DOTNET
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
#elif WIN_APPS
        public async Task<Stream> CreateAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(targetPath);

            return await file.OpenStreamForWriteAsync();
        }

        public async Task<Stream> OpenReadAsync(string targetPath)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(targetPath);

            return await file.OpenStreamForWriteAsync();
        }

        public Task DeleteAsync(string targetPath)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
