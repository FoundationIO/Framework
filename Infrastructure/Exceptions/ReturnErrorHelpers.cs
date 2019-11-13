/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Collections.Generic;

namespace Framework.Infrastructure.Exceptions
{
    public static class ReturnErrorHelpers
    {
        public static List<ReturnErrorItem> ToReturnErrorItemList(this List<string> errorList, string defaultKey = "")
        {
            var result = new List<ReturnErrorItem>();
            foreach (var item in errorList)
                result.Add(new ReturnErrorItem { Key = defaultKey, Value = item });
            return result;
        }
    }
}
