/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
#pragma warning disable CS0612 // Type or member is obsolete

using FluentMigrator.Runner.Processors;
using Framework.Infrastructure.Models.Config;
using LinqToDB.DataProvider;

namespace Framework.Data.DbAccess
{
    public interface IDBInfo
    {
        DbConnectionInfo GetDbSettings();

        string GetConnectionString(bool useMasterDB = false);

        IDataProvider GetDBProvider();

        MigrationProcessorFactory GetMigrationProcessorFactory();
    }
}

#pragma warning restore CS0612 // Type or member is obsolete
