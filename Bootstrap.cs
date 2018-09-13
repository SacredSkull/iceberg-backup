using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Iceberg.Service.Backup;
using Iceberg.Service.Hashing;
using Iceberg.Service.Hashing.Storage;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Iceberg {
    class Bootstrap {
        private static IContainer container;
        static void Main(string[] args) {
            Configure();

            Iceberg iceberg = container.Resolve<Iceberg>();
            iceberg.Run();
        }

        static void Configure() {
            var builder = new ContainerBuilder();
            var serviceCol = new ServiceCollection();

            builder.Populate(serviceCol);
            builder.RegisterType<FileHashStorage>().As<HashStorage>();
            builder.RegisterType<MD5Service>().As<HashService>();
            builder.RegisterType<GoogleColdLineBackup>().As<BackupService>();
            builder.RegisterType<Iceberg>().AsSelf();
            builder.RegisterInstance(Logging()).As<ILogger>();

            container = builder.Build();
        }

        static Logger Logging() {
             // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
            var consoleTarget = new ColoredConsoleTarget("console")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("filelogs")
            {
                FileName = "${basedir}/iceberg.log",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            // Step 3. Define rules
            config.AddRuleForOneLevel(LogLevel.Error, fileTarget); // only errors to file
            config.AddRuleForAllLevels(consoleTarget); // all to console

            // Step 4. Activate the configuration
            LogManager.Configuration = config;

            return LogManager.GetLogger("Iceberg");
        }
    }
}
