using System.IO;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage.FileSystem
{
    public interface IFileSystemAccess
    {
        Task<Stream> CreateAsync(string targetPath);
        Task<Stream> OpenReadAsync(string targetPath);
        Task DeleteAsync(string targetPath);
    }
}
