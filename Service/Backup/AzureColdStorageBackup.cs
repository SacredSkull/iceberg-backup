using System.Collections.Generic;
using Iceberg.Entity;
using NLog;

namespace Iceberg.Service.Backup {
    public class AzureColdStorageBackup : BackupService {
        public AzureColdStorageBackup(ILogger logger) : base(logger) { }

        public override void Setup() {
            throw new System.NotImplementedException();
        }
        public override void Push(IEnumerable<IcebergFile> files) {
            throw new System.NotImplementedException();
        }
    }
}