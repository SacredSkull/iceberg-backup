using System.IO;

namespace Iceberg.Entity {
	public class HashedFile {
		public HashedFile(IcebergFile file, Hash hash) {
			File = file;
			Hash = hash;
		}

		public IcebergFile File {
			get;
			protected set;
		}
		
		public Hash Hash {
			get;
			protected set;
		}
	}
}
