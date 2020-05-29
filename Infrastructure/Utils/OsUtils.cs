/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Runtime.InteropServices;

namespace Framework.Infrastructure.Utils
{
    public static class OsUtils
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static bool IsFreeBSD() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

        public static bool IsUnixBasedOS() =>
            (IsMacOS() || IsLinux() || IsFreeBSD()) && (!IsWindows());

        public static string ReplacePathSeperators(string path)
        {
            if (path.IsTrimmedStringNullOrEmpty())
                return path;

            if (IsWindows())
            {
                return path.Replace("/", "\\");
            }
            else
            {
                return path.Replace("\\", "/");
            }
        }
    }
}
