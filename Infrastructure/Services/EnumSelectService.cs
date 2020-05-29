/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Interfaces.Services;
using Framework.Infrastructure.Models;
using Framework.Infrastructure.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace Framework.Infrastructure.Services
{
    public class EnumSelectService : IEnumSelectService
    {
        private readonly IMemoryCache memoryCache;

        public EnumSelectService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public List<TextWithValue> GetSelectList<T>()
            where T : Enum
        {
            var typ = typeof(T);
            var enumName = typ.Name;
            if (memoryCache.TryGetValue<List<TextWithValue>>(enumName, out List<TextWithValue> result))
                return result;
            result = new List<TextWithValue>();

            if (!typ.IsEnum)
                return result;
            var enumValues = System.Enum.GetValues(typ);
            Type underlyingType = Enum.GetUnderlyingType(typ);

            foreach (T val in enumValues)
            {
                var actualValue = Convert.ChangeType(val, underlyingType);
                result.Add(new TextWithValue() { Text = Enum.GetName(typeof(T), val).SplitCamelCase(), Value = actualValue.ToString() });
            }

            memoryCache.Set(enumName, result);
            return result;
        }

        public List<TextWithValue> GetSelectListWithAll<T>()
            where T : Enum
        {
            var lst = new List<TextWithValue>(GetSelectList<T>());
            lst.Insert(0, new TextWithValue() { Text = "All", Value = "" });
            return lst;
        }
    }
}
