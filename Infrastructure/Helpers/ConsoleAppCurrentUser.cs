/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Threading.Tasks;
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Interfaces.Helpers;
using Framework.Infrastructure.Utils;

namespace Framework.Infrastructure.Helpers
{
    public class ConsoleAppCurrentUser : ICurrentUser
    {
        private readonly string appName;

        public ConsoleAppCurrentUser(IBaseConfiguration baseConfiguration)
        {
            appName = baseConfiguration.AppName;
        }

        public long GetNotNullableCurrentUserId()
        {
            return SafeUtils.Long(GetCurrentUserId());
        }

        public long? GetCurrentUserId()
        {
            return null;
        }

        public string GetCurrentUserName()
        {
            return $"App-{appName}";
        }

        public string GetCurrentUserToken()
        {
            return null;
        }

        public string GetCurrentUserAddress()
        {
            return Environment.MachineName;
        }

        public string GetCurrentUserPropertyAsString(string propertyName)
        {
            return null;
        }

        public long GetCurrentUserPropertyAsLong(string propertyName)
        {
            return SafeUtils.Long(GetCurrentUserPropertyAsString(propertyName), 0);
        }

        public T GetCurrentUserProperty<T>(string propertyName)
        {
            var v = GetCurrentUserPropertyAsString(propertyName);

            if (v.IsTrimmedStringNullOrEmpty())
                return default(T);

            return (T)Convert.ChangeType(v, typeof(T));
        }

        public string GetCurrentClaimProperty(string propertyName)
        {
            return null;
        }

        public long GetCurrentClaimPropertyAsLong(string propertyName)
        {
            return 0;
        }

        public Task<string> GetAuthenticationTokenAsync()
        {
            return Task.FromResult<string>((string)null);
        }
    }
}
