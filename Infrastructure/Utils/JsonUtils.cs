/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Text.Json;

namespace Framework.Infrastructure.Utils
{
    public static class JsonUtils
    {
        public static T DeserializeWithoutPropertyNaming<T>(string jsonStr)
        {
            return JsonSerializer.Deserialize<T>(jsonStr);
        }

        public static string SerializeWithoutPropertyNaming<T>(T obj, bool formatted = false)
        {
            return JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = formatted
            });
        }

        public static T Deserialize<T>(string jsonStr)
        {
            return JsonSerializer.Deserialize<T>(jsonStr, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            });
        }

        public static string Serialize<T>(T obj, bool formatted = false)
        {
            return JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
                WriteIndented = formatted
            });
        }
    }
}
