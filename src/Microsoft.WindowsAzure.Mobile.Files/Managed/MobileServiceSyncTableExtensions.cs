// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async static Task DeleteFileAsync<T>(this IMobileServiceSyncTable<T> table, T dataItem, string fileName)
        {
            var context = GetManagedSyncContext(table);
            var file = Files.MobileServiceSyncTableExtensions.CreateFile(table, dataItem, fileName);
            await context.DeleteFileAsync(file);
        }

        // return the derived class to avoid needing to import the Files namespace and introduce collisions
        public async static Task<IEnumerable<MobileServiceManagedFile>> GetFilesAsync<T>(this IMobileServiceSyncTable<T> table, T dataItem)
        {
            return (await Files.MobileServiceSyncTableExtensions.GetFilesAsync(table, dataItem)).Select(MobileServiceManagedFile.FromMobileServiceFile);
        }

        // make push and pull available on this namespace
        public static Task PushFileChangesAsync<T>(this IMobileServiceSyncTable<T> table)
        {
            return Files.MobileServiceSyncTableExtensions.PushFileChangesAsync(table);
        }

        public static Task PullFilesAsync<T>(this IMobileServiceSyncTable<T> table, T dataItem)
        {
            return Files.MobileServiceSyncTableExtensions.PullFilesAsync(table, dataItem);
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
