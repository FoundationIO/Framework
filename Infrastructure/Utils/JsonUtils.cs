/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Framework.Infrastructure.Utils
{
    public static class JsonUtils
    {
        public static T Deserialize<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static string Serialize(object obj)
        {
            ITraceWriter traceWriter = new MemoryTraceWriter();

            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
                {
                    TraceWriter = traceWriter,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
