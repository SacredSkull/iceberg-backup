using System;
using System.Collections.Generic;
using System.IO;
using Iceberg.Entity;
using Iceberg.Service.Hashing;
using Iceberg.Service.Backup;
using NLog;

namespace Iceberg {
    public class Iceberg {
        private HashService hasher;
        private ILogger logger;
        private BackupService backup;
        private HashSet<IcebergFile> changedFiles = new HashSet<IcebergFile>();
        public Iceberg(HashService hasher, BackupService backup, ILogger logger) {
            this.hasher = hasher;
            this.logger = logger;
            this.backup = backup;
        }

        public void Run() {
            VerifyFiles();
            UploadChanges();
        }

        private void UploadChanges() {
            backup.Push(changedFiles);
        }

        private void VerifyFiles() {
            foreach(var file in Directory.GetFiles(Environment.CurrentDirectory)) {
                if(hasher.HashStorage.ShouldIgnore(file)) // We don't care about this (right now)
                    continue;
                
                if(hasher.HashExists(file)) {
                    // A checksum exists for this file, but is is valid? really though?
                    var possibleNonMatch = new IcebergFile(file);
                    if(!hasher.VerifyStoredChecksum(possibleNonMatch)) {
                        logger.Warn($"MISMATCH! {possibleNonMatch.FileInfo.Name} does not match stored hash");
                        hasher.WriteHashFile(possibleNonMatch);
                        changedFiles.Add(possibleNonMatch);
                    }
                } else {
                    // This appears to be a new file.
                    var newfile = new IcebergFile(file);
                    hasher.WriteHashFile(newfile);
                    changedFiles.Add(newfile);
                    logger.Info($"Encountered a new file: {newfile.FileInfo.Name}");
                }
            }

            if(changedFiles.Count == 0)
                logger.Info("Nothing to do (Your wallet is safe... for now!)");
            else
                logger.Info($"Found {changedFiles.Count} file(s) that will need to be uploaded.");
        }
    }
}