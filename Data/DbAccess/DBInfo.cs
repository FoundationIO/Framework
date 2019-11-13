/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
#pragma warning disable CS0612 // Type or member is obsolete

using System;
using System.Linq;
using System.Threading;
using FluentMigrator.Runner.Processors;
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Models.Config;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.MySql;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.DataProvider.SqlServer;

namespace Framework.Data.DbAccess
{
    public abstract class DBInfo : IDBInfo
    {
        private static readonly SqlServerDataProvider SqlServerProvider = new SqlServerDataProvider("default", SqlServerVersion.v2008);
        private static readonly SQLiteDataProvider Sqlite3Provider = new SQLiteDataProvider();
        private static readonly MySqlDataProvider MySqlProvider = new MySqlDataProvider();
        private readonly IBaseConfiguration config;
        private readonly string profileName;

        protected DBInfo(string profileName, IBaseConfiguration config)
        {
            this.profileName = profileName;
            this.config = config;
        }

        public virtual DbConnectionInfo GetDbSettings()
        {
            return GetDbSettings(profileName);
        }

        public virtual DbConnectionInfo GetDbSettings(string currentProfile)
        {
            if (!config.ConnectionSettings.Keys.Any(x => x == currentProfile))
            {
                throw new Exception($"Unable to find DB Provider for the Database Connection profile {currentProfile}");
            }

            return config.ConnectionSettings[currentProfile];
        }

        public virtual string GetConnectionString(string currentProfile)
        {
            if (!config.ConnectionSettings.Keys.Any(x => x == currentProfile))
            {
                throw new Exception($"Unable to find DB Provider for the Database Connection profile {currentProfile}");
            }

            var dbConnectionInfo = config.ConnectionSettings[currentProfile];

            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);

            var connectionStr = string.Empty;

            var dbType = (dbConnectionInfo.DatabaseType ?? string.Empty).Trim().ToLower();
            switch (dbType)
            {
                case DBType.MYSQL:
                    {
                        if (dbConnectionInfo.DatabaseUseIntegratedLogin)
                            connectionStr = $"IntegratedSecurity=yes;Server={dbConnectionInfo.DatabaseServer};Database={dbConnectionInfo.DatabaseName};";
                        else
                            connectionStr = $"Server={dbConnectionInfo.DatabaseServer};Database={dbConnectionInfo.DatabaseName};Uid={dbConnectionInfo.DatabaseUserName};Pwd={dbConnectionInfo.DatabasePassword};";
                        break;
                    }

                case DBType.SQLSERVER:
                    {
                        if (dbConnectionInfo.DatabaseUseIntegratedLogin)
                            connectionStr = $"Integrated Security=true;Server={dbConnectionInfo.DatabaseServer};Initial Catalog={dbConnectionInfo.DatabaseName};Persist Security Info=True;MultipleActiveResultSets =False;Application Name={config.AppName};Max Pool Size={workerThreads};";
                        else
                            connectionStr = $"Server={dbConnectionInfo.DatabaseServer};Initial Catalog={dbConnectionInfo.DatabaseName};Persist Security Info=True;User ID={dbConnectionInfo.DatabaseUserName};Password={dbConnectionInfo.DatabasePassword};MultipleActiveResultSets=False;Application Name={config.AppName};Max Pool Size={workerThreads};";
                        break;
                    }

                case DBType.SQLITE3:
                    {
                        connectionStr = $"Data Source={dbConnectionInfo.DatabaseName}";
                        break;
                    }

                default:
                    {
                        throw new Exception($"Unable to get Configuration string, Unknown Database type specified in the configuration {dbConnectionInfo.DatabaseType}");
                    }
            }

            return connectionStr;
        }

        public virtual string GetConnectionString()
        {
            return GetConnectionString(profileName);
        }

        public virtual MigrationProcessorFactory GetMigrationProcessorFactory()
        {
            return GetMigrationProcessorFactory(profileName);
        }

        public virtual MigrationProcessorFactory GetMigrationProcessorFactory(string currentProfile)
        {
            if (!config.ConnectionSettings.Keys.Any(x => x == currentProfile))
            {
                throw new Exception($"Unable to find DB Provider for the Database Connection profile {currentProfile}");
            }

            var dbConnectionInfo = config.ConnectionSettings[currentProfile];

            var dbType = (dbConnectionInfo.DatabaseType ?? string.Empty).Trim().ToLower();
            switch (dbType)
            {
                case DBType.SQLSERVER:
                    {
                        return new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
                    }

                case DBType.SQLITE3:
                    {
                        return new FluentMigrator.Runner.Processors.SQLite.SQLiteProcessorFactory();
                    }

                case DBType.MYSQL:
                    {
                        return new FluentMigrator.Runner.Processors.MySql.MySql5ProcessorFactory();
                    }

                default:
                    {
                        throw new Exception($"Unable to get Migration Process Factory, Unknown Database type specified in the configuration {dbConnectionInfo.DatabaseType}");
                    }
            }
        }

        public virtual IDataProvider GetDBProvider()
        {
            return GetDBProvider(this.profileName);
        }

        public virtual IDataProvider GetDBProvider(string currentProfile)
        {
            if (!config.ConnectionSettings.Keys.Any(x => x == currentProfile))
            {
                throw new Exception($"Unable to find DB Provider for the Database Connection profile {currentProfile}");
            }

            var dbConnectionInfo = config.ConnectionSettings[currentProfile];

            var dbType = (dbConnectionInfo.DatabaseType ?? string.Empty).Trim().ToLower();
            switch (dbType)
            {
                case DBType.MYSQL:
                    {
                        return MySqlProvider;
                    }

                case DBType.SQLSERVER:
                    {
                        return SqlServerProvider;
                    }

                case DBType.SQLITE3:
                    {
                        return Sqlite3Provider;
                    }

                default:
                    {
                        throw new Exception($"Unable to get DB Provider, Unknown Database type specified in the configuration {dbConnectionInfo.DatabaseType}");
                    }
            }

            throw new Exception(string.Format("DB Type {0} is not supported yet", dbConnectionInfo.DatabaseType));
        }
    }
}

#pragma warning restore CS0612 // Type or member is obsolete
