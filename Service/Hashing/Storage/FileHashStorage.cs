using System.IO;
using Iceberg.Entity;
using NLog;

namespace Iceberg.Service.Hashing.Storage {
    public class FileHashStorage : HashStorage
    {
        protected const string CHECKSUM_FILE_ENDING = ".iceberg";

        public FileHashStorage(ILogger logger) : base(logger) { }

        public override bool Exists(string path) => File.Exists(path + CHECKSUM_FILE_ENDING);

        public override bool Exists(HashedFile hashed) => Exists(hashed.File);
        public override bool Exists(IcebergFile file) => Exists(file);

        public override Hash Load(IcebergFile file)
        {
            try {
                using(BinaryReader reader = new BinaryReader(File.Open(file.Path + CHECKSUM_FILE_ENDING, FileMode.Open)) ) {
                    var val = new Hash(reader.ReadString());
                    return val;
                } 
            } catch(IOException e) {
                logger.Fatal($"Completely failed to read a hash ({file})! {e.Message} {e.InnerException}");
                throw e;
            }
        }

        public override bool Write(HashedFile hashfile)
        {
            try {
                string checksumPath = $"{hashfile.File}{CHECKSUM_FILE_ENDING}";
                using(BinaryWriter checksum = new BinaryWriter(new FileStream(checksumPath, FileMode.OpenOrCreate)))
                    checksum.Write(hashfile.Hash.ToString());
                
                File.SetAttributes(checksumPath, FileAttributes.Hidden);
                return true;
            } catch(IOException e) {
                logger.Error($"Could not write hash! {e.Message} {e.InnerException}");
                return false;
            }
        }

        public override bool ShouldIgnore(string path) => path.EndsWith(CHECKSUM_FILE_ENDING);
    }
}