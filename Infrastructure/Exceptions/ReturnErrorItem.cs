/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Collections.Generic;

namespace Framework.Infrastructure.Exceptions
{
    public class ReturnErrorItem
    {
        public ReturnErrorItem()
        {
        }

        public ReturnErrorItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public static List<ReturnErrorItem> AsReturnErrorList(string key, string value)
        {
            return new List<ReturnErrorItem>
            {
                new ReturnErrorItem { Key = key, Value = value }
            };
        }
    }
}
