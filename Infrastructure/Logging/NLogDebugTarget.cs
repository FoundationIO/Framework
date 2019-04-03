/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Diagnostics;
using NLog;
using NLog.Targets;

namespace Framework.Infrastructure.Logging
{
    [Target("NLogDebugTarget")]
    public sealed class NLogDebugTarget : TargetWithLayout
    {
        public NLogDebugTarget()
        {
        }

        protected override void Write(LogEventInfo logEvent)
        {
            string logMessage = this.Layout.Render(logEvent);
            Debug.WriteLine(logMessage);
        }
    }
}
