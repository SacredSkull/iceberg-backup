using Iceberg.Entity;
using NLog;

namespace Iceberg.Service.Hashing.Storage {
    public abstract class HashStorage {
        protected ILogger logger;
        public HashStorage(ILogger logger) {
            this.logger = logger;
        }

        public abstract Hash Load(IcebergFile file);

        public abstract bool Write(HashedFile hash);

        public abstract bool Exists(IcebergFile file);
        public abstract bool Exists(HashedFile file);
        public abstract bool Exists(string path);

        public abstract bool ShouldIgnore(string path);
    }
}