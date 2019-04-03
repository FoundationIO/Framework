/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Constants
{
    public static class Strings
    {
        public static class Config
        {
            public const string LogTrace = "logTrace";
            public const string LogDebug = "logDebug";
            public const string LogInfo = "logInfo";
            public const string LogSql = "logSql";
            public const string LogWarn = "logWarn";
            public const string LogError = "logError";
            public const string LogToFile = "logToFile";
            public const string LogLocation = "logLocation";
            public const string ConfigPath = "|ConfigPath|";
            public const string LogPerformance = "logPerformance";
            public const string LogToDebugger = "logToDebugger";
            public const string LogToConsole = "logToConsole";
            public const string DatabaseType = "databaseType";
            public const string DatabaseName = "databaseName";
            public const string DatabaseServer = "databaseServer";
            public const string DatabaseUserName = "databaseUserName";
            public const string DatabasePassword = "databasePassword";
            public const string DatabaseCommandTimeout = "databaseCommandTimeout";
            public const string MaxPoolSize = "maxPoolSize";
            public const string AutomaticMigration = "automaticMigration";
            public const string MigrationNamespace = "migrationNamespace";
            public const string ServerPort = "serverPort";
            public const string LogLevel = "LogLevel";
            public const string DbSettings = "dbSettings";
            public const string LogSettings = "logSettings";
        }

        public static class Log
        {
            public const string Critical = "CRITICAL";
            public const string Trace = "TRACE";
            public const string Error = "ERROR";
            public const string Warning = "WARNING";
            public const string Info = "INFO";
            public const string Fatal = "FATAL";
            public const string Debug = "DEBUG";
            public const string SqlBeginTransaction = "TRANS-BEGIN";
            public const string SqlCommitTransaction = "TRANS-COMMIT";
            public const string SqlRollbackTransaction = "TRANS-ROLLBACK";
            public const string Sql = "SQL";
            public const string SqlError = "SQL-ERROR";
        }
    }
}
