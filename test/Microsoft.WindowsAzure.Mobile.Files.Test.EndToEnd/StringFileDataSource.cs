using Microsoft.WindowsAzure.MobileServices.Files;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd
{
    class StringFileDataSource : IMobileServiceFileDataSource
    {
        private readonly string Source;

        public StringFileDataSource(string source)
        {
            Source = source;
        }

        public async Task<Stream> GetStream()
        {
            return new MemoryStream(Source.Select(x => (byte)x).ToArray());
        }
    }
}
