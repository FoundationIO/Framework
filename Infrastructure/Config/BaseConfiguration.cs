/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Models.Config;
using Framework.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;

namespace Framework.Infrastructure.Config
{
    public class BaseConfiguration : IBaseConfiguration
    {
        public BaseConfiguration()
        {
            ConnectionSettings = new Dictionary<string, DbConnectionInfo>();
        }

        //General App Related
        public string AppName { get; set; } = null;

        public Dictionary<string, DbConnectionInfo> ConnectionSettings { get; private set; }

        // public DbSettings DbSettings { get; private set; }

        // log related
        public LogSettings LogSettings { get; private set; }

        protected void PrepareFolders()
        {
            if (!Directory.Exists(LogSettings.LogLocation))
            {
                Directory.CreateDirectory(LogSettings.LogLocation);
            }

            foreach (var conn in this.ConnectionSettings)
            {
                var settings = conn.Value;

                if (settings.DatabaseType == DBType.SQLITE3)
                {
                    if (!Directory.Exists(FileUtils.GetFileDirectory(settings.DatabaseName)))
                    {
                        Directory.CreateDirectory(FileUtils.GetFileDirectory(settings.DatabaseName));
                    }
                }
            }
        }

        protected virtual string GetConfigFileLocation()
        {
            return string.Empty;
        }

        protected void PopulateFromConfigFile(IConfigurationSection appSettings, IConfigurationSection frameworkLogSetting, IConfigurationSection connectionSettings, string configLocation)
        {
            //Load .Net core's Logging configuration
            var otherLogSettings = new List<KeyValuePair<string, Microsoft.Extensions.Logging.LogLevel>>();
            var logLevelSettings = frameworkLogSetting.GetSection(Strings.Config.LogLevel);
            foreach (var setting in logLevelSettings.GetChildren())
            {
                var logLevel = SafeUtils.Enum<Microsoft.Extensions.Logging.LogLevel>(setting.Value, Microsoft.Extensions.Logging.LogLevel.None);
                otherLogSettings.Add(new KeyValuePair<string, Microsoft.Extensions.Logging.LogLevel>(setting.Key, logLevel));
            }

            var logSection = appSettings.GetSection(Strings.Config.LogSettings);
            // var ls = logSection.GetChildren().Select(x => x.Key);

            LogSettings = new LogSettings(logSection, otherLogSettings, (str) =>
            {
                if (str.IsTrimmedStringNotNullOrEmpty() && str.Contains(Strings.Config.ConfigPath))
                {
                    str = str.Replace(Strings.Config.ConfigPath, FileUtils.GetFileDirectory(configLocation));
                    str = Path.GetFullPath(new Uri(str).LocalPath);
                }
                return str;
            });

            int count = 0;
            //var ls = connectionSettings.GetChildren().Select(x => x.Key);
            // Console.WriteLine(ls.Count());

            foreach (var child in connectionSettings.GetChildren())
            {
                count++;
                var childDbInfo = new DbConnectionInfo
                {
                    Name = child.Key
                };

                if (childDbInfo.Name.IsTrimmedStringNullOrEmpty())
                {
                    throw new Exception($"Profile - {count} does not have name");
                }

                if (ConnectionSettings.Keys.Count(x => x == childDbInfo.Name) > 1)
                {
                    throw new Exception($"Profile - {count} has duplicate profile name");
                }

                childDbInfo.DatabaseName = child["databaseName"] ?? "";
                childDbInfo.DatabaseType = child["databaseType"] ?? "";
                childDbInfo.DatabaseServer = child["databaseServer"] ?? "";

                var str = childDbInfo.DatabaseServer;
                if (str.IsTrimmedStringNotNullOrEmpty() && str.Contains(Strings.Config.ConfigPath))
                {
                    str = str.Replace(Strings.Config.ConfigPath, FileUtils.GetFileDirectory(configLocation));
                    str = Path.GetFullPath(new Uri(str).LocalPath);
                    childDbInfo.DatabaseServer = str;
                }

                str = childDbInfo.DatabaseName;
                if (str.IsTrimmedStringNotNullOrEmpty() && str.Contains(Strings.Config.ConfigPath))
                {
                    str = str.Replace(Strings.Config.ConfigPath, FileUtils.GetFileDirectory(configLocation));
                    str = Path.GetFullPath(new Uri(str).LocalPath);
                    childDbInfo.DatabaseName = str;
                }

                childDbInfo.DatabaseUserName = child["databaseUserName"] ?? "";
                childDbInfo.DatabasePassword = child["databasePassword"] ?? "";

                childDbInfo.DatabaseUseIntegratedLogin = SafeUtils.Bool(child["databaseUseIntegratedLogin"], false);

                childDbInfo.DatabaseCommandTimeout = SafeUtils.Int(child["databaseCommandTimeout"], 100);
                childDbInfo.MaxPoolSize = SafeUtils.Int(child["maxPoolSize"], 1000);
                childDbInfo.AdditionalParameters = child["additionalParameters"] ?? "";

                childDbInfo.MigrationNamespace = child["migrationNamespace"] ?? "";
                childDbInfo.MigrationProfile = child["migrationProfile"] ?? "";

                ConnectionSettings.Add(childDbInfo.Name, childDbInfo);

                if (SafeUtils.Bool(child["ignoreValidation"], false))
                {
                    continue;
                }

                if (childDbInfo.DatabaseServer.IsTrimmedStringNullOrEmpty())
                {
                    throw new Exception($"Profile - {count} does not have database server");
                }
            }

            /*
            DbSettings = new DbSettings(appSettings.GetSection(Strings.Config.DbSettings), (str) =>
            {
                if (str.IsTrimmedStringNotNullOrEmpty() && str.Contains(Strings.Config.ConfigPath))
                {
                    str = str.Replace(Strings.Config.ConfigPath, FileUtils.GetFileDirectory(configLocation));
                    str = Path.GetFullPath(new Uri(str).LocalPath);
                }
                return str;
            });
            */
            AppName = Path.GetFileNameWithoutExtension(GetType().GetTypeInfo().Assembly.Location);
        }
    }
}
