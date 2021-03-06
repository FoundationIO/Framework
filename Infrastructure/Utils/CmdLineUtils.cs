﻿/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Utils
{
    public static class CmdLineUtils
    {
        public static bool IsParamAvailable(this string[] args, string paramName)
        {
            if (args == null)
            {
                return false;
            }

            for (var i = 0; i < args.Length; ++i)
            {
                var argItem = args[i].ToLower().Trim();
                paramName = paramName.ToLower().Trim();

                if (argItem == paramName || argItem == "/" + paramName || argItem == "\\" + paramName
                    || argItem == "--" + paramName || argItem == "-" + paramName)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsParamValueAvailable(this string[] args, string paramName)
        {
            if (args == null)
            {
                return false;
            }

            for (var i = 0; i < args.Length; ++i)
            {
                var argItem = args[i].ToLower().Trim();
                paramName = paramName.ToLower().Trim();

                if (argItem == paramName || argItem == "/" + paramName || argItem == "\\" + paramName
                    || argItem == "--" + paramName || argItem == "-" + paramName)
                {
                    if (i + 1 <= args.Length)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetParamValueAsString(this string[] args, string paramName, string defaultValue = "")
        {
            if (args == null)
            {
                return defaultValue;
            }

            for (var i = 0; i < args.Length; ++i)
            {
                var argItem = args[i].ToLower().Trim();
                paramName = paramName.ToLower().Trim();

                if (argItem == paramName || argItem == "/" + paramName || argItem == "\\" + paramName
                    || argItem == "--" + paramName || argItem == "-" + paramName)
                {
                    if (i + 1 < args.Length)
                    {
                        var val = args[i + 1];
                        return val;
                    }
                }
            }

            return defaultValue;
        }

        public static T GetParamValueAs<T>(this string[] args, string paramName, T defaultValue)
        {
            var paramValue = GetParamValueAsString(args, paramName);
            try
            {
                var t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                return (T)Convert.ChangeType(paramValue, t);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
