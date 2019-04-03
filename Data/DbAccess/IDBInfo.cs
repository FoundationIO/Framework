/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using FluentMigrator.Runner.Processors;
using Framework.Infrastructure.Models.Config;
using LinqToDB.DataProvider;

namespace Framework.Data.DbAccess
{
    public interface IDBInfo
    {
        //string GetConnectionString();

        //IDataProvider GetDBProvider();

        //MigrationProcessorFactory GetMigrationProcessorFactory();

        DbConnectionInfo GetDbSettings();

        string GetConnectionString();

        IDataProvider GetDBProvider();

        MigrationProcessorFactory GetMigrationProcessorFactory();
    }
}