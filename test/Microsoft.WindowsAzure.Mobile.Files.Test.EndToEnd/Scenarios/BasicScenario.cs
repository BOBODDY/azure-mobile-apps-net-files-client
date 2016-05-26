using Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Infrastructure;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Scenarios
{
    [Trait("End to end: Basic scenarios", "")]
    public class BasicScenario
    {
        private readonly DataEntity item = new DataEntity { Id = "1" };
        private readonly MobileServiceFile file = new MobileServiceFile("test.txt", "DataEntity", "1");
        private readonly Dictionary<string, string> fileContent = new Dictionary<string, string>
        {
            { "test.txt", "This is a test" }
        };

        [Fact]
        public async Task BlobCanBeUploadedListedRetrievedDeleted()
        {
            await ExecuteAndClearStore(async table =>
            {
                // add the file
                await table.AddFileAsync(file);

                // test our local store before syncing
                var files = await table.GetFilesAsync(item);
                Assert.Equal(1, files.Count());
                Assert.Equal("test.txt", files.ElementAt(0).Name);

                // sync
                await table.PushFileChangesAsync();
            });

            await ExecuteAndClearStore(async table =>
            {
                // test our local store after syncing
                await table.PullFilesAsync(item);
                var files = await table.GetFilesAsync(item);
                Assert.Equal(1, files.Count());
                Assert.Equal("test.txt", files.ElementAt(0).Name);

                // download the file and test content
                await table.DownloadFileAsync(file, "test.txt");
                var content = File.ReadAllText("test.txt");
                Assert.Equal(fileContent["test.txt"], content);
                File.Delete("test.txt");

                // delete the file
                await table.DeleteFileAsync(file);
                await table.PushFileChangesAsync();
            });

            await ExecuteAndClearStore(async table =>
            {
                // make sure we really deleted the file
                await table.PullFilesAsync(item);
                var files = await table.GetFilesAsync(item);
                Assert.Equal(0, files.Count());
            });
        }

        private async Task ExecuteAndClearStore(Func<IMobileServiceSyncTable<DataEntity>, Task> test)
        {
            using (var store = new MobileServiceSQLiteStore("test.sqlite"))
            {
                var table = await GetTableAsync(store);
                await test(table);
            }
            File.Delete("test.sqlite");
        }

        private async Task<IMobileServiceSyncTable<DataEntity>> GetTableAsync(MobileServiceSQLiteStore store)
        {
            store.DefineTable<DataEntity>();
            var client = new MobileServiceClient("http://localhost:3000/");
            client.InitializeFileSyncContext(new StringFileSyncHandler(fileContent), store);
            await client.SyncContext.InitializeAsync(store);
            return client.GetSyncTable<DataEntity>();
        }
    }
}
