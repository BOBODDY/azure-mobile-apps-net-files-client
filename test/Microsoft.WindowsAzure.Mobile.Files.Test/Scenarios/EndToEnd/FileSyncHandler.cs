using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.Scenarios.EndToEnd
{
    class FileSyncHandler : IFileSyncHandler
    {
        public Task<IMobileServiceFileDataSource> GetDataSource(MobileServiceFileMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public Task ProcessFileSynchronizationAction(MobileServiceFile file, FileSynchronizationAction action)
        {
            throw new NotImplementedException();
        }
    }
}
