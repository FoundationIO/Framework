/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Framework.Infrastructure.Config
{
    public class InMemoryJsonConfigFileProvider : IFileProvider
    {
        private readonly IFileInfo _fileInfo;

        public InMemoryJsonConfigFileProvider(string json) => _fileInfo = new InMemoryFile(json);

        public IFileInfo GetFileInfo(string subpath) => _fileInfo;

        public IDirectoryContents GetDirectoryContents(string subpath) => (IDirectoryContents)null;

        public IChangeToken Watch(string filter) => NullChangeToken.Singleton;

        private class InMemoryFile : IFileInfo
        {
            private readonly byte[] _data;

            public InMemoryFile(string json) => _data = Encoding.UTF8.GetBytes(json);

            public long Length => _data.Length;

            public bool Exists { get; } = true;

            public string PhysicalPath { get; } = string.Empty;

            public string Name { get; } = string.Empty;

            public DateTimeOffset LastModified { get; } = DateTimeOffset.UtcNow;

            public bool IsDirectory { get; } = false;

            public Stream CreateReadStream() => new MemoryStream(_data);
        }
    }
}
