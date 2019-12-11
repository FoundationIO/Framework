/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Reflection;

namespace Framework.Data.Migrations
{
    public interface IDBMigration
    {
        bool IsMigrationUptoDate(Assembly migrationAssembly = null);

        bool MigrateToLatestVersion(Assembly migrationAssembly = null);

        Assembly GetMigrationAssembly();
    }
}