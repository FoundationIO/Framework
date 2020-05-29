/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.IO;

namespace Framework.Infrastructure.Utils
{
    public static class ConfigUtils
    {
        public static T GetEnvironmentVariableOrDefault<T>(string[] args, string environmentVar, T defaultValue)
        {
            var resultStr = args.GetParamValueAsString(environmentVar);
            if (resultStr == null || resultStr == "")
            {
                resultStr = Environment.GetEnvironmentVariable(environmentVar);
                if (resultStr == null || resultStr == "")
                {
                    return ReflectionUtils.GetValueFromString<T>(resultStr, defaultValue);
                }
            }

            return ReflectionUtils.GetValueFromString<T>(resultStr, defaultValue);
        }

        public static string GetConfigFileName(string baseConfigFileName, bool useCurrentDirectory = false)
        {
            string path;
            var dir = "..";
            for (int i = 0; i < 8; ++i)
            {
                path = useCurrentDirectory ? FileUtils.GetCurrentDirectory() : FileUtils.GetApplicationExeDirectory();
                for (int j = 0; j < i; ++j)
                {
                    path = FileUtils.Combine(path, dir);
                }

                path = FileUtils.Combine(path, "Configuration", baseConfigFileName);

                if (File.Exists(path))
                {
                    return path;
                }
            }

            for (int i = 0; i < 8; ++i)
            {
                path = useCurrentDirectory ? FileUtils.GetCurrentDirectory() : FileUtils.GetApplicationExeDirectory();
                for (int j = 0; j < i; ++j)
                {
                    path = FileUtils.Combine(path, dir);
                }

                path = FileUtils.Combine(path, baseConfigFileName);

                if (File.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}
