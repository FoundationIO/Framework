/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using DatabaseSchemaReader.DataSchema;

namespace Framework.Utilities.PocoGenerator
{
    public class TemplateData
    {
        public Config Config { get; set; }

        public DatabaseSchema DatabaseSchema { get; set; }
    }
}
