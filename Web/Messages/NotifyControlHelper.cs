/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Microsoft.AspNetCore.Mvc;

namespace Framework.Web.Messages
{
    public static class NotifyControlHelper
    {
        public static void ShowErrorMsg(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.ErrorMsg] = errorMsg;
        }

        public static void ShowInfoMsg(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.InfoMsg] = errorMsg;
        }

        public static void ShowWarningMsg(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.WarningMsg] = errorMsg;
        }

        public static void ShowSuccessMsg(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.SuccessMsg] = errorMsg;
        }

        public static void ShowErrorMsgWithCaption(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.ShowCaption] = true;
            ShowErrorMsg(controller, errorMsg);
        }

        public static void ShowInfoMsgWithCaption(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.ShowCaption] = true;
            ShowInfoMsg(controller, errorMsg);
        }

        public static void ShowWarningMsgWithCaption(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.ShowCaption] = true;
            ShowWarningMsg(controller, errorMsg);
        }

        public static void ShowSuccessMsgWithCaption(this Controller controller, string errorMsg)
        {
            controller.TempData[NotifyMessageContants.ShowCaption] = true;
            ShowSuccessMsg(controller, errorMsg);
        }
    }
}