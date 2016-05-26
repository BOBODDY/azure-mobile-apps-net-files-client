using Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Infrastructure;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Scenarios.Managed
{
    [Trait("End to end: Managed online", "")]
    public class OnlineScenario
    {
        private readonly DataEntity item = new DataEntity { Id = "1" };

        [Fact(DisplayName = "Files can be added, retrieved and deleted")]
        public async Task BasicScenario()
        {
            var table = GetTable();

            // add a file
            await table.AddFileAsync(item, "test.txt", GetStream("this is a test"));

            // make sure it appears in a list
            var files = await table.GetFilesAsync(item);
            Assert.Equal(1, files.Count());

            // make sure the content can be retrieved
            var retrievedStream = await table.GetFileAsync(item, "test.txt");
            Assert.Equal("this is a test", new StreamReader(retrievedStream).ReadToEnd());

            // delete and make sure it no longer appears in a list
            await table.DeleteFileAsync(item, "test.txt");
            files = await table.GetFilesAsync(item);
            Assert.Equal(0, files.Count());
        }

        private IMobileServiceTable<DataEntity> GetTable()
        {
            var client = new MobileServiceClient("http://localhost:3000/");
            return client.GetTable<DataEntity>();
        }

        private Stream GetStream(string source)
        {
            return new MemoryStream(source.Select(x => (byte)x).ToArray());
        }
    }
}
