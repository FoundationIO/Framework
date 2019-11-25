/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Utils;

namespace Framework.Infrastructure.Exceptions
{
    public class ReturnError
    {
        public ReturnError()
        {
        }

        public ReturnError(string friendlyMessage, List<ReturnErrorItem> errorItemList = null, string internalErrorMessage = null)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = internalErrorMessage ?? friendlyMessage;
            ErrorItemList = errorItemList;
            Exception = null;
        }

        public ReturnError(string friendlyMessage, List<ReturnErrorItem> errorItemList = null, Exception ex = null, string internalErrorMessage = null)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = internalErrorMessage ?? friendlyMessage;
            ErrorItemList = errorItemList;
            if (ex != null)
                Exception = new ReturnException(ex);
        }

        public ReturnError(Exception ex)
        {
            if (ex != null)
            {
                FriendlyMessage = ex.Message;
                InternalErrorMessage = ex.Message;
                ErrorItemList = null;
                Exception = new ReturnException(ex);
            }
        }

        public ReturnError(string friendlyMessage, Exception ex, string internalMessage = null)
        {
            FriendlyMessage = friendlyMessage;
            InternalErrorMessage = internalMessage ?? friendlyMessage;
            ErrorItemList = null;
            if (ex != null)
                Exception = new ReturnException(ex);
        }

        public string FriendlyMessage { get; set; }

        public string InternalErrorMessage { get; set; }

        public List<ReturnErrorItem> ErrorItemList { get; set; }

        public ReturnException Exception { get; set; }

        public string AllErrorAsString()
        {
            return JsonUtils.Serialize(this);
        }
    }
}
