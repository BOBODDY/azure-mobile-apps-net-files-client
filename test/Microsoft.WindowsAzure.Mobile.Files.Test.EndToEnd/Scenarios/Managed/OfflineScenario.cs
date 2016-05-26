using Microsoft.WindowsAzure.Mobile.Files.Managed;
using Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Infrastructure;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Scenarios.Managed
{
    [Trait("End to end: Managed online", "")]
    public class OfflineScenario
    {
        private readonly DataEntity item = new DataEntity { Id = "1" };
        private readonly Stream file = new MemoryStream("this is a test".Select(x => (byte)x).ToArray());

        [Fact(DisplayName = "Files can be added, retrieved and deleted")]
        public async Task BasicScenario()
        {
            await ExecuteAndClearStore(async table =>
            {
                // add the file
                await table.AddFileAsync(item, "test.txt", file);

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
                await table.GetFileAsync(item, "test.txt");
                var content = File.ReadAllText("test.txt");
                Assert.Equal("this is a test", content);
                File.Delete("test.txt");

                // delete the file
                await table.DeleteFileAsync(item, "test.txt");
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
            client.InitializeManagedFileSyncContext(store);
            await client.SyncContext.InitializeAsync(store);
            return client.GetSyncTable<DataEntity>();
        }
    }
}
