using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using Iceberg.Entity;
using NLog;
using Iceberg.Service.Hashing.Storage;

namespace Iceberg.Service.Hashing {
	public class MD5Service : HashService {
		public MD5Service(ILogger logger, HashStorage storage) : base(logger, storage) {}
		public override Hash Hash(Stream stream) => new Hash(Encoding.Default.GetString(MD5.Create().ComputeHash(stream)));

		public override void Dispose() { }
	}
}
