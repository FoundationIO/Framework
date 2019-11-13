/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Framework.Infrastructure.Logging
{
    public class PerformanceLog : IDisposable
    {
        private readonly string module;
        private readonly string function;
        private readonly bool autoCloseIsError;
        private readonly ILog log;
        private readonly bool logToDefaultLogger;
        private bool started = false;
        private DateTime startTime;
        private bool disposed;

        public PerformanceLog(ILog log, string moduleName, string functionName, bool startMeasuringOnCreate, bool autoCloseIsError, bool logToDefaultLogger = true)
        {
            module = moduleName;
            function = functionName;
            this.autoCloseIsError = autoCloseIsError;
            this.log = log;
            this.logToDefaultLogger = logToDefaultLogger;
            if (startMeasuringOnCreate)
            {
                Start();
            }
        }

        public void Start()
        {
            if (!started)
            {
                startTime = DateTime.Now;
                started = true;
            }
        }

        public void StopAndWriteCompleteLog(string additionalMsg = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            StopAndWriteToLog("Completed", additionalMsg, sourceLineNumber, memberName, sourceFilePath);
        }

        public void StopAndWriteErrorLog(string additionalMsg = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            StopAndWriteToLog("Error", additionalMsg, sourceLineNumber, memberName, sourceFilePath);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (started)
                    {
                        if (autoCloseIsError)
                        {
                            StopAndWriteErrorLog();
                        }
                        else
                        {
                            StopAndWriteCompleteLog();
                        }
                    }
                }

                disposed = true;
            }
        }

        private void StopAndWriteToLog(string status = "completed", string additionalMsg = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            started = false;
            var endTime = DateTime.Now;
            if (logToDefaultLogger)
            {
                log.Info($"Module = {module} , Function = {function}  , startTime  =  {startTime} , endTime = {endTime} , additionalMsg = {additionalMsg}", sourceLineNumber, memberName, sourceFilePath);
            }

            log.Performance(module, function, startTime, endTime, new List<KeyValuePair<string, object>>(), 1, status, additionalMsg, sourceLineNumber, memberName, sourceFilePath);
        }
    }
}