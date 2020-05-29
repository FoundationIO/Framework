/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Threading.Tasks;

namespace Framework.Infrastructure.Interfaces.Helpers
{
    public interface ICurrentUser
    {
        long GetNotNullableCurrentUserId();

        long? GetCurrentUserId();

        string GetCurrentUserName();

        string GetCurrentUserToken();

        string GetCurrentUserAddress();

        string GetCurrentUserPropertyAsString(string propertyName);

        long GetCurrentUserPropertyAsLong(string propertyName);

        T GetCurrentUserProperty<T>(string propertyName);

        string GetCurrentClaimProperty(string propertyName);

        long GetCurrentClaimPropertyAsLong(string propertyName);

        Task<string> GetAuthenticationTokenAsync();
    }
}
