// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.Mobile.Files.Managed;
using Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Infrastructure;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files.Express;
using Microsoft.WindowsAzure.MobileServices.Files.Express.LocalStorage.FileSystem;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd.Scenarios.Managed
{
    [Trait("End to end: Managed metadata", "")]
    public class MetadataScenario
    {
        private readonly DataEntity item = new DataEntity { Id = "1" };
        private readonly Stream fileStream = new MemoryStream("Managed metadata scenario".Select(x => (byte)x).ToArray());

        [Fact(DisplayName = "Metadata is attached to files returned from GetFiles")]
        public async Task BasicScenario()
        {
            await ExecuteAndClearStore(async table =>
            {
                await table.AddFileAsync(item, "test.txt", fileStream);

                // make sure PhysicalPath is attached
                var files = await table.GetFilesAsync(item);
                Assert.Equal(1, files.Count());
                var file = (MobileServiceFileSystemFile) files.ElementAt(0);
                Assert.Equal(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataEntity-1-test.txt"), file.PhysicalPath);
                Assert.Equal("test.txt", file.Name);

                // make sure PhysicalPath is correct!
                Assert.Equal("Managed metadata scenario", File.ReadAllText(file.PhysicalPath));

                await table.DeleteFileAsync(item, "test.txt");
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
