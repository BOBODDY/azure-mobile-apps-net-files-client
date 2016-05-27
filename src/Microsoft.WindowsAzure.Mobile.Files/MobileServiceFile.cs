// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;

namespace Microsoft.WindowsAzure.MobileServices.Files
{
    public class MobileServiceFile
    {
        internal MobileServiceFile() { }

        public MobileServiceFile(string name, string tableName, string parentId)
            : this(name, name, tableName, parentId) { }

        public MobileServiceFile(string id, string name, string tableName, string parentId)
        {
            this.Id = id;
            this.Name = name;
            this.TableName = tableName;
            this.ParentId = parentId;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string TableName { get; set; }

        public string ParentId { get; set; }

        public long Length { get; set; }

        public string ContentMD5 { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string StoreUri { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        internal static MobileServiceFile FromMetadata(MobileServiceFileMetadata metadata)
        {
            var file = new MobileServiceFile(metadata.FileId, metadata.ParentDataItemType, metadata.ParentDataItemId);

            file.ContentMD5 = metadata.ContentMD5;
            file.LastModified = metadata.LastModified;
            file.Length = metadata.Length;
            file.Metadata = metadata.ToDictionary();
            return file;
        }
    }
}
