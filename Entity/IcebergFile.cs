using System;
using System.Collections.Generic;
using System.IO;

namespace Iceberg.Entity {
    public class IcebergFile : IEqualityComparer<IcebergFile>, IEquatable<IcebergFile>, IComparer<IcebergFile> {
        public IcebergFile(string path) {
            Path = path;
        }
        public string Path { get; protected set;}

        private FileInfo _fileInfo;
        public FileInfo FileInfo { 
            get => _fileInfo ?? (_fileInfo = new FileInfo(Path));
        }

        public override string ToString() => Path;

        public bool Equals(IcebergFile x, IcebergFile y) => x.Path.Equals(y.Path);

        public int GetHashCode(IcebergFile obj) => obj.Path.GetHashCode();

        public bool Equals(IcebergFile other) => Path.Equals(other.Path);

        public int Compare(IcebergFile x, IcebergFile y) => x.Path.CompareTo(y.Path);
    }
}