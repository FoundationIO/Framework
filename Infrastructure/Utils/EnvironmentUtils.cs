using System;

namespace Framework.Infrastructure.Utils
{
    public static class EnvironmentUtils
    {
        public static string GetEnvironmentVariableFromAllPlaces(string environmentVariable)
        {
            var str = Environment.GetEnvironmentVariable(environmentVariable);
            if (!str.IsTrimmedStringNullOrEmpty())
                return str;

            str = Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.Process);
            if (!str.IsTrimmedStringNullOrEmpty())
                return str;

            str = Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.User);
            if (!str.IsTrimmedStringNullOrEmpty())
                return str;

            str = Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.Machine);
            if (!str.IsTrimmedStringNullOrEmpty())
                return str;

            return string.Empty;
        }

        public static T GetEnvironmentVariableFromAllPlaces<T>(string environmentVariable, T defaultValue = default(T))
            where T : struct
        {
            try
            {
                var str = GetEnvironmentVariableFromAllPlaces(environmentVariable);
                return (T)Convert.ChangeType(str, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
