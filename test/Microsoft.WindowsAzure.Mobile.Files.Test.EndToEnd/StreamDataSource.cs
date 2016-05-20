using Microsoft.WindowsAzure.MobileServices.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Microsoft.WindowsAzure.Mobile.Files.Test.EndToEnd
{
    class StreamDataSource : IMobileServiceFileDataSource
    {
        private readonly string Source;

        public StreamDataSource(string source)
        {
            Source = source;
        }

        public async Task<Stream> GetStream()
        {
            return new MemoryStream(Source.Select(x => (byte)x).ToArray());
        }
    }
}
