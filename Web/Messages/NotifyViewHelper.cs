/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Framework.Web.Messages
{
    public static class NotifyViewHelper
    {
        public static bool IsErrorMsgSet(this ViewContext helper)
        {
            return (helper.TempData != null) && helper.TempData.Keys.Any(item => item.Equals(NotifyMessageContants.ErrorMsg));
        }

        public static bool IsErrorMsgSet(this IHtmlHelper helper)
        {
            return helper.ViewContext.IsErrorMsgSet();
        }

        public static bool IsInfoMsgSet(this ViewContext helper)
        {
            return (helper.TempData != null) && helper.TempData.Keys.Any(item => item.Equals(NotifyMessageContants.InfoMsg));
        }

        public static bool IsInfoMsgSet(this IHtmlHelper helper)
        {
            return helper.ViewContext.IsInfoMsgSet();
        }

        public static bool IsWarningMsgSet(this ViewContext helper)
        {
            return (helper.TempData != null) && helper.TempData.Keys.Any(item => item.Equals(NotifyMessageContants.WarningMsg));
        }

        public static bool IsWarningMsgSet(this IHtmlHelper helper)
        {
            return helper.ViewContext.IsWarningMsgSet();
        }

        public static bool IsSuccessMsgSet(this ViewContext helper)
        {
            return (helper.TempData != null) && helper.TempData.Keys.Any(item => item.Equals(NotifyMessageContants.SuccessMsg));
        }

        public static bool IsSuccessMsgSet(this IHtmlHelper helper)
        {
            return helper.ViewContext.IsSuccessMsgSet();
        }

        public static bool IsShowCaptionSet(this ViewContext helper)
        {
            return (helper.TempData != null) && helper.TempData.Keys.Any(item => item.Equals(NotifyMessageContants.ShowCaption));
        }

        public static bool IsShowCaptionSet(this IHtmlHelper helper)
        {
            return helper.ViewContext.IsShowCaptionSet();
        }

        public static HtmlString ShowErrorMsgIfSet(this ViewContext helper)
        {
            if (IsErrorMsgSet(helper))
            {
                return NotifyHtmlHelper.DisplayErrorMsg((string)helper.TempData[NotifyMessageContants.ErrorMsg]);
            }

            return new HtmlString(string.Empty);
        }

        public static HtmlString ShowInfoMsgIfSet(this ViewContext helper)
        {
            if (IsInfoMsgSet(helper))
            {
                return NotifyHtmlHelper.DisplayInfoMsg((string)helper.TempData[NotifyMessageContants.InfoMsg]);
            }

            return new HtmlString(string.Empty);
        }

        public static HtmlString ShowWarningMsgIfSet(this ViewContext helper)
        {
            if (IsWarningMsgSet(helper))
            {
                return NotifyHtmlHelper.DisplayWarningMsg((string)helper.TempData[NotifyMessageContants.WarningMsg]);
            }

            return new HtmlString(string.Empty);
        }

        public static HtmlString ShowSuccessMsgIfSet(this ViewContext helper)
        {
            if (IsSuccessMsgSet(helper))
            {
                return NotifyHtmlHelper.DisplaySuccessMsg((string)helper.TempData[NotifyMessageContants.SuccessMsg]);
            }

            return new HtmlString(string.Empty);
        }

        public static HtmlString ShowAllMessages(this ViewContext helper)
        {
            var sb = new StringBuilder();
            sb.Append(ShowErrorMsgIfSet(helper));
            sb.Append(string.Empty);
            sb.Append(ShowInfoMsgIfSet(helper));
            sb.Append(string.Empty);
            sb.Append(ShowWarningMsgIfSet(helper));
            sb.Append(string.Empty);
            sb.Append(ShowSuccessMsgIfSet(helper));
            sb.Append(string.Empty);
            return new HtmlString(sb.ToString());
        }
    }
}
