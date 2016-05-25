using System.IO;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.LocalStorage
{
    public interface ILocalStorageProvider
    {
        Task AddAsync(MobileServiceFile file, Stream source);
        Task<Stream> GetAsync(MobileServiceFile file);
        Task DeleteAsync(MobileServiceFile file);
    }
}
