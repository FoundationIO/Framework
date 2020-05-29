/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using Framework.Data.DbAccess;
using Framework.Infrastructure.Logging;
using Framework.Infrastructure.Models.Config;
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete

namespace Framework.Data.Migrations
{
    public abstract class DBMigration : IDBMigration
    {
        private readonly ILog log;
        private readonly IDBInfo dbInfo;
        private readonly DbConnectionInfo dbSettings;

        protected DBMigration(ILog log, IDBInfo dbInfo)
        {
            this.log = log;
            this.dbInfo = dbInfo;
            dbSettings = dbInfo.GetDbSettings();
        }

        public bool IsMigrationUptoDate(Assembly migrationAssembly = null)
        {
            var announcer = new TextWriterAnnouncer(x => System.Diagnostics.Debug.WriteLine(x));
            Assembly assembly;

            if (migrationAssembly == null)
            {
                assembly = GetMigrationAssembly(dbSettings.MigrationNamespace);
                if (assembly == null)
                {
                    log.Error($"Unable to find the namespace {dbSettings.MigrationNamespace} in the loaded assemblies");
                    throw new Exception($"Unable to find the namespace {dbSettings.MigrationNamespace} in the loaded assemblies");
                }
            }
            else
            {
                assembly = migrationAssembly;
            }

            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = dbSettings.MigrationNamespace,
                Profile = dbSettings.MigrationProfile == "" ? null : dbSettings.MigrationProfile,
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = dbInfo.GetMigrationProcessorFactory();
            using (var processor = factory.Create(dbInfo.GetConnectionString(), announcer, options))
            {
                var runner = new MigrationRunner(assembly, migrationContext, processor);
                if (runner.MigrationLoader.LoadMigrations()
                    .Any(pair => !runner.VersionLoader.VersionInfo.HasAppliedMigration(pair.Key)))
                {
                    return false;
                }
            }

            return true;
        }

        public bool MigrateToLatestVersion(Assembly migrationAssembly = null)
        {
            var announcer = new TextWriterAnnouncer(s => log.Info(s));
            Assembly assembly;

            if (migrationAssembly == null)
            {
                assembly = GetMigrationAssembly(dbSettings.MigrationNamespace);
                if (assembly == null)
                {
                    log.Error($"Unable to find the namespace {dbSettings.MigrationNamespace} in the loaded assemblies");
                    throw new Exception($"Unable to find the namespace {dbSettings.MigrationNamespace} in the loaded assemblies");
                }
            }
            else
            {
                assembly = migrationAssembly;
            }

            var migrationContext = new RunnerContext(announcer)
            {
                Profile = dbSettings.MigrationProfile,
                Namespace = dbSettings.MigrationNamespace
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = dbInfo.GetMigrationProcessorFactory();
            using (var processor = factory.Create(dbInfo.GetConnectionString(), announcer, options))
            {
                var runner = new MigrationRunner(assembly, migrationContext, processor);
                runner.MigrateUp(true);
            }

            return true;
        }

        public abstract Assembly GetMigrationAssembly();

        private Assembly GetMigrationAssembly(string migrationNamespace)
        {
            var alist = from a in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != "Microsoft.IntelliTrace.Core") select a;

            foreach (var item in alist)
            {
                log.Info($"assembly  = {item.FullName}");
            }

            var qry = from a in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != "Microsoft.IntelliTrace.Core")
                      from t in a.GetTypes()
                      where t.Namespace != null && t.Namespace.ToLower().Equals(migrationNamespace.ToLower().Trim())
                      select a;
            return qry.FirstOrDefault();
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
