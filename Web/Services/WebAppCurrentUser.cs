/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.Helpers;
using Framework.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
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
            return httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? $"App-{baseConfiguration.AppName}";
        }

        public string GetCurrentUserToken()
        {
            return null;
        }

        public string GetCurrentUserAddress()
        {
            return httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        public string GetCurrentUserPropertyAsString(string propertyName)
        {
            if (propertyName == null)
                return null;

            if (httpContextAccessor?.HttpContext?.User?.Identity?.Name == null)
                return null;

            var key = httpContextAccessor?.HttpContext.Request.Query.Keys.FirstOrDefault(x => x.ToUpper().Trim() == propertyName.Trim().ToUpper());
            if (key != null)
                return httpContextAccessor?.HttpContext.Request.Query[key];

            //If value does not exists in Querystring, lets check the Header
            key = httpContextAccessor?.HttpContext.Request.Headers.Keys.FirstOrDefault(x => x.ToUpper().Trim() == propertyName.Trim().ToUpper());
            if (key != null)
                return httpContextAccessor?.HttpContext.Request.Headers[key];

            return null;
        }

        public long GetCurrentUserPropertyAsLong(string propertyName)
        {
            return SafeUtils.Long(GetCurrentUserPropertyAsString(propertyName));
        }

        public T GetCurrentUserProperty<T>(string propertyName)
        {
            var v = GetCurrentUserPropertyAsString(propertyName);

            if (v.IsTrimmedStringNullOrEmpty())
                return default(T);
            return SafeUtils.To<T>(v);
        }

        public string GetCurrentClaimProperty(string propertyName)
        {
            if (propertyName == null)
                return null;

            if (httpContextAccessor?.HttpContext?.User?.Identity?.Name == null)
                return null;

            if (httpContextAccessor?.HttpContext?.User?.Claims.IsNullOrEmpty() == false)
            {
                var claim = httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type.ToUpper().Trim() == propertyName.Trim().ToUpper());
                if (claim != null)
                    return claim.Value;
            }

            return null;
        }

        public long GetCurrentClaimPropertyAsLong(string propertyName)
        {
            return SafeUtils.Long(GetCurrentClaimProperty(propertyName));
        }

        public async Task<string> GetAuthenticationTokenAsync()
        {
            var token = await httpContextAccessor?.HttpContext.GetTokenAsync(HttpHeaderConstants.AccessToken);
            return token;
        }
    }
}
