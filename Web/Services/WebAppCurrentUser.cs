/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;

namespace Framework.Web.Helpers
{
    public class WebAppCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBaseConfiguration baseConfiguration;

        public WebAppCurrentUser(IHttpContextAccessor httpContextAccessor, IBaseConfiguration baseConfiguration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.baseConfiguration = baseConfiguration;
        }

        public string GetCurrentUserName()
        {
            return httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? $"App-{baseConfiguration.AppName}";
        }
    }
}
