/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Attributes
{
    public class TypeTableAttribute : Attribute
    {
        public TypeTableAttribute(string tableName, Type typeOfParent = null)
        {
            TableName = tableName;
            TypeOfParent = typeOfParent;
        }

        public string TableName { get; private set; }

        public Type TypeOfParent { get; private set; }
    }
}
