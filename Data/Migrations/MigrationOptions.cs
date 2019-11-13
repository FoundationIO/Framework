/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
#pragma warning disable CS0612 // Type or member is obsolete
using FluentMigrator;

namespace Framework.Data.Migrations
{
    public class MigrationOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }

        public string ProviderSwitches { get; set; }

        public int? Timeout { get; set; }
    }
}
#pragma warning restore CS0612 // Type or member is obsolete
