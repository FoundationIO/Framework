/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Exceptions
{
    public class ReturnException
    {
        public ReturnException()
        {
        }

        public ReturnException(Exception ex)
        {
            if (ex != null)
            {
                StackTrace = ex.StackTrace;
                Source = ex.Source;
                Message = ex.Message;
                if (ex.InnerException != null)
                    InnerException = new ReturnException(ex.InnerException);
            }
        }

        public string StackTrace { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public ReturnException InnerException { get; set; }
    }
}
