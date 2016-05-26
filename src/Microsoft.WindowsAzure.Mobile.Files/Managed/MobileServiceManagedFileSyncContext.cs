using Microsoft.WindowsAzure.MobileServices.Files.Managed.LocalStorage;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Operations;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Files.Sync.Triggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files.Managed
{
    public class MobileServiceManagedFileSyncContext : MobileServiceFileSyncContext
    {
        private ILocalStorageProvider localStorage;

        public MobileServiceManagedFileSyncContext(IMobileServiceClient client, IFileMetadataStore metadataStore, IFileOperationQueue operationsQueue,
            IFileSyncTriggerFactory syncTriggerFactory, IFileSyncHandler syncHandler, IMobileServiceFilesClient filesClient, ILocalStorageProvider localStorage) 
            : base(client, metadataStore, operationsQueue, syncTriggerFactory, syncHandler, filesClient)
        {
            this.localStorage = localStorage;
        }

        public async Task<Stream> GetFileAsync(MobileServiceFile file)
        {
            return await localStorage.GetAsync(file);
        }

        public async Task AddFileAsync(MobileServiceFile file, Stream stream)
        {
            await base.AddFileAsync(file);
            await localStorage.AddAsync(file, stream);
        }
    }
}
