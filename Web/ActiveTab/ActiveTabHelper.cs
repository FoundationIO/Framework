/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Framework.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Framework.Web.ActiveTab
{
    public static class ActiveTabHelper
    {
        private const string ActiveDataViewDataName = "ActiveTabId";

        public static bool IsActiveTab(this IHtmlHelper html, int tabNumber)
        {
            var st = html.ViewContext.ViewData[ActiveDataViewDataName] as string;
            return SafeUtils.Int(st, -1000) == tabNumber;
        }

        public static void SetActiveTab(this Controller controller, int tabNumber)
        {
            controller.ViewData[ActiveDataViewDataName] = tabNumber;
        }
    }
}
