/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using Framework.Data.DbAccess;
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Logging;

namespace Framework.Data.Migrations
{
    public class MigrationOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }

        public string ProviderSwitches { get; set; }

        public int? Timeout { get; set; }
    }
}
