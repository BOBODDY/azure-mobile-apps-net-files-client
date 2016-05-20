using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd
{
    class FileSyncHandler : IFileSyncHandler
    {
        private readonly Dictionary<string, string> Sources;

        public FileSyncHandler(Dictionary<string, string> sources)
        {
            Sources = sources;
        }

        public async Task<IMobileServiceFileDataSource> GetDataSource(MobileServiceFileMetadata metadata)
        {
            return new StreamDataSource(Sources[metadata.FileName]);
        }

        public async Task ProcessFileSynchronizationAction(MobileServiceFile file, FileSynchronizationAction action)
        {

        }
    }
}
