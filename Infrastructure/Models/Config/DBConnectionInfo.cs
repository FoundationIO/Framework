/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
namespace Framework.Infrastructure.Models.Config
{
    public class DbConnectionInfo
    {
        public string Name { get; set; }

        public string DatabaseType { get; set; }

        public string DatabaseName { get; set; }

        public string DatabaseServer { get; set; }

        public string DatabaseUserName { get; set; }

        public string DatabasePassword { get; set; }

        public int DatabaseCommandTimeout { get; set; }

        public int MaxPoolSize { get; set; }

        public bool DatabaseUseIntegratedLogin { get; set; }

        public string AdditionalParameters { get; set; }

        public string MigrationProfile { get; set; }

        public string MigrationNamespace { get; set; }
    }
}
