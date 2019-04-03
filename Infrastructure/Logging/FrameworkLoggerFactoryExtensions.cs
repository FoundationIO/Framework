/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using Framework.Infrastructure.Config;
using Microsoft.Extensions.Logging;

namespace Framework.Infrastructure.Logging
{
    public static class FrameworkLoggerFactoryExtensions
    {
        public static ILoggerFactory AddFrameworkLogger(this ILoggerFactory loggerFactory, IBaseConfiguration config, ILog log)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            loggerFactory.AddProvider(new FrameworkLoggerProvider(config, log));
            return loggerFactory;
        }
    }
}
