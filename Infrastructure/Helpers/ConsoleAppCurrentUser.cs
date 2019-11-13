/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Interfaces.Helpers;

namespace Framework.Infrastructure.Helpers
{
    public class ConsoleAppCurrentUser : ICurrentUser
    {
        private readonly string appName;

        public ConsoleAppCurrentUser(IBaseConfiguration baseConfiguration)
        {
            appName = baseConfiguration.AppName;
        }

        public string GetCurrentUserName()
        {
            return $"App-{appName}";
        }
    }
}
