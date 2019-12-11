/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.IO;
using System.Threading.Tasks;
using Framework.Data.DbAccess;
using Framework.Infrastructure.Constants;
using LinqToDB.Data;

namespace Framework.Data.Utils
{
    public abstract class DatabaseUtils : IDatabaseUtils
    {
        private readonly IDBInfo dBInfo;

        protected DatabaseUtils(IDBInfo dBInfo)
        {
            this.dBInfo = dBInfo;
        }

        private enum DbOperationType
        {
            CreateDB,
            DeleteDB
        }

        public bool IsDatabaseNeedToBeRecreated()
        {
            return dBInfo.GetDbSettings().AlwaysCreateNewDatabase;
        }

        public Task<bool> CreateDatabaseAsync()
        {
            return ExecuteCommandAsync(DbOperationType.CreateDB);
        }

        public Task<bool> DeleteDatabaseAsync()
        {
            return ExecuteCommandAsync(DbOperationType.DeleteDB);
        }

        private async System.Threading.Tasks.Task<bool> ExecuteCommandAsync(DbOperationType dbOperationType)
        {
            using (var dbConn = new DataConnection(dBInfo.GetDBProvider(), dBInfo.GetConnectionString(true)))
            {
                var sqlStr = "";
                bool skipSqlOperation = false;

                switch (dBInfo.GetDbSettings().DatabaseType)
                {
                    case DBType.MYSQL:
                        {
                            if (dbOperationType == DbOperationType.CreateDB)
                            {
                                sqlStr = $"CREATE DATABASE [{dBInfo.GetDbSettings().DatabaseName}];";
                            }
                            else if (dbOperationType == DbOperationType.DeleteDB)
                            {
                                sqlStr = $"DROP DATABASE IF EXISTS {dBInfo.GetDbSettings().DatabaseName};";
                            }

                            break;
                        }

                    case DBType.SQLSERVER:
                        {
                            if (dbOperationType == DbOperationType.CreateDB)
                            {
                                sqlStr = $"CREATE DATABASE [{dBInfo.GetDbSettings().DatabaseName}]";
                            }
                            else if (dbOperationType == DbOperationType.DeleteDB)
                            {
                                sqlStr = $"ALTER DATABASE [{dBInfo.GetDbSettings().DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE GO DROP DATABASE [{dBInfo.GetDbSettings().DatabaseName}]";
                            }

                            break;
                        }

                    case DBType.SQLITE3:
                        {
                            skipSqlOperation = true;
                            if (dbOperationType == DbOperationType.DeleteDB)
                            {
                                var dbFile = dBInfo.GetDbSettings().DatabaseName;
                                if (File.Exists(dbFile))
                                    File.Delete(dbFile);
                            }

                            break;
                        }

                    default:
                        {
                            throw new Exception($"Unable to get Configuration string, Unknown Database type specified in the configuration {dBInfo.GetDbSettings().DatabaseType}");
                        }
                }

                if (!skipSqlOperation)
                    await dbConn.ExecuteAsync(sqlStr);

                return true;
            }
        }
    }
}
