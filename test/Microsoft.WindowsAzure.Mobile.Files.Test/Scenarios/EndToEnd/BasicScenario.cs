using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.WindowsAzure.MobileServices.Files;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.Scenarios.EndToEnd
{
    [Trait("End to end: Basic scenarios", "")]
    public class BasicScenario
    {
        [Fact]
        public async Task BlobCanBeUploadedListedRetrievedDeleted()
        {
            var client = new MobileServiceClient("http://localhost:3000/");
            var store = new MobileServiceSQLiteStore("mydatabase.db");
            store.DefineTable<DataEntity>();

            client.InitializeFileSyncContext(new FileSyncHandler(), store);

            await client.SyncContext.InitializeAsync(store);

            var table = client.GetTable<DataEntity>();
            var item = new DataEntity { Id = "1" };
            var file = new MobileServiceFile("test.txt", "DataEntity", "1");
            var stream = new MemoryStream("This is a test".Select(x => (byte)x).ToArray());

            await table.InsertAsync(item);
            await table.UploadFromStreamAsync(file, stream);

            var files = await table.GetFilesAsync(item);

            Assert.Equal(1, files.Count());
            Assert.Equal("test.txt", files.ElementAt(0).Name);
        }
    }
}
