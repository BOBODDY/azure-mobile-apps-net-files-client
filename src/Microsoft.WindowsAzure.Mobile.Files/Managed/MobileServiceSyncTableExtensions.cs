using Microsoft.WindowsAzure.MobileServices.Files.Managed;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed
{
    public static class MobileServiceSyncTableExtensions
    {
        public async static Task AddFileAsync<T>(this IMobileServiceSyncTable<T> table, T dataItem, string fileName, Stream stream)
        {
            var context = GetManagedSyncContext(table);
            var file = Files.MobileServiceSyncTableExtensions.CreateFile(table, dataItem, fileName);
            await context.AddFileAsync(file, stream);
        }

        public async static Task<Stream> GetFileAsync<T>(this IMobileServiceSyncTable<T> table, T dataItem, string fileName)
        {
            var context = GetManagedSyncContext(table);
            var file = Files.MobileServiceSyncTableExtensions.CreateFile(table, dataItem, fileName);
            return await context.GetFileAsync(file);
        }

        private static MobileServiceManagedFileSyncContext GetManagedSyncContext<T>(IMobileServiceSyncTable<T> table)
        {
            var syncContext = table.MobileServiceClient.GetFileSyncContext() as MobileServiceManagedFileSyncContext;
            if (syncContext == null)
            {
                throw new InvalidOperationException("To use AddFileAsync with a Stream object, you must initialize files by calling InitializeManagedFileSyncContext()");
            }
            return syncContext;
        }
    }
}
