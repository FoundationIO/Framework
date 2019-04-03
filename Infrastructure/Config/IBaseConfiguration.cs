/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Collections.Generic;
using Framework.Infrastructure.Models.Config;

namespace Framework.Infrastructure.Config
{
    public interface IBaseConfiguration
    {
        string AppName { get; }

        LogSettings LogSettings { get; }

        Dictionary<string, DbConnectionInfo> ConnectionSettings { get; }

        // DbSettings DbSettings { get; }
    }
}
