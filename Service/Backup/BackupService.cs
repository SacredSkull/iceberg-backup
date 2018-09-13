using System;
using System.Collections.Generic;
using Iceberg.Entity;
using NLog;

namespace Iceberg.Service.Backup {
	public abstract class BackupService {
		protected ILogger logger;
		public BackupService(ILogger logger) {
			this.logger = logger;
		}
		public abstract void Setup();
		public abstract void Push(IEnumerable<IcebergFile> files);
	}
}
