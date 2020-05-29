/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Infrastructure.Utils
{
    public static class ValidationUtils
    {
        public static bool IsGuidValid(object guid)
        {
            var g = SafeUtils.Guid(guid);
            return g != Guid.Empty;
        }

        public static bool IsEmail(string str)
        {
            return new EmailAddressAttribute().IsValid(str);
        }

        public static bool IsEnumValid<T, TValue>(TValue enumValue)
            where T : Enum
        {
            var typeValue = SafeUtils.Enum<T, TValue>(enumValue, null);
            return Enum.IsDefined(typeof(T), typeValue);
        }

        public static bool IsEnumValid<T>(List<string> enumValues)
        {
            var res = enumValues.SelectToList(x => Enum.IsDefined(typeof(T), SafeUtils.Enum<T>(x, null)));
            if (res.Contains(false))
                return false;
            return true;
        }
    }
}
