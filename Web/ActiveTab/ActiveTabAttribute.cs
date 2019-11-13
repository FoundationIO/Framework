/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Framework.Web.ActiveTab
{
    public class ActiveTabAttribute : ActionFilterAttribute
    {
        private readonly int tabIndex;

        public ActiveTabAttribute(int idx)
        {
            tabIndex = idx;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.SetActiveTab(tabIndex);
            }

            base.OnActionExecuting(context);
        }
    }
}
