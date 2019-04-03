/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Web.Routing
{
    public static class RouteValueDictionaryExtensions
    {
        public static RouteValueDictionary AddQueryStringParameters(this RouteValueDictionary dict, HttpContext context)
        {
            var querystring = context.Request.Query;

            foreach (var item in querystring)
                if (!dict.ContainsKey(item.Key))
                {
                    dict.Add(item.Key, item.Value[0]);
                }

            return dict;
        }

        public static RouteValueDictionary ExceptFor(this RouteValueDictionary dict, params string[] keysToRemove)
        {
            foreach (var key in keysToRemove)
                if (dict.ContainsKey(key))
                {
                    dict.Remove(key);
                }

            return dict;
        }
    }
}