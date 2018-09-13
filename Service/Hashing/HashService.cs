using System;
using System.IO;
using Iceberg.Entity;
using Iceberg.Service.Hashing.Storage;
using NLog;

namespace Iceberg.Service.Hashing {
	public abstract class HashService : IDisposable {
		protected ILogger logger;
		public HashStorage HashStorage { get; protected set; }
		public HashService(ILogger logger, HashStorage storage) {
			this.logger = logger;
			this.HashStorage = storage;
		}

		public Hash WriteHashFile(IcebergFile file) {
            HashedFile hf = new HashedFile(file, CreateHashFromContents(file));
			HashStorage.Write(hf);
			return hf.Hash;
		}

		public Hash WriteHashFile(string path) => WriteHashFile(new IcebergFile(path));

		public Hash LoadStoredHash(IcebergFile file) {
			return HashStorage.Load(file);
		}

		public bool HashExists(string file) => HashStorage.Exists(file);

		protected Hash CreateHashFromContents(IcebergFile file) {
			using(var stream = file.FileInfo.Open(FileMode.Open))
				return Hash(stream);
		}
		public abstract Hash Hash(Stream data);
		public virtual bool VerifyStoredChecksum(IcebergFile volatileFile) => Verify(LoadStoredHash(volatileFile), CreateHashFromContents(volatileFile));
		public virtual bool Verify(Hash hash1, Hash hash2) => hash1.Equals(hash2);
		public abstract void Dispose();
	}
}
 