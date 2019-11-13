/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Attributes
{
    public class ParentKeyAttribute : Attribute
    {
        public ParentKeyAttribute(string parentKeyColumn, long parentKeyValue)
        {
            ParentKeyColumn = parentKeyColumn;
            ParentKeyValue = parentKeyValue;
        }

        public string ParentKeyColumn { get; private set; }

        public long ParentKeyValue { get; private set; }
    }
}
