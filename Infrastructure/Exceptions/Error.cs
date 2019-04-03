/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;

namespace Framework.Infrastructure.Exceptions
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string friendlyMessage, string internalErrorMessage = null, List<ErrorItem> errorItemList = null, Exception ex = null)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = internalErrorMessage ?? friendlyMessage;
            ErrorItemList = errorItemList;
            Exception = ex;
        }

        public Error(string friendlyMessage, List<ErrorItem> errorItemList = null, Exception ex = null)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = friendlyMessage;
            ErrorItemList = errorItemList;
            Exception = null;
        }

        public Error(Exception ex)
        {
            FriendlyMessage = ex.Message;
            InternalErrorMessage = ex.Message;
            ErrorItemList = null;
            Exception = ex;
        }

        public Error(string friendlyMessage, Exception ex)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = friendlyMessage;
            ErrorItemList = null;
            Exception = ex;
        }

        public string FriendlyMessage { get; set; }

        public string InternalErrorMessage { get; set; }

        public List<ErrorItem> ErrorItemList { get; set; }

        public Exception Exception { get; set; }
    }
}
