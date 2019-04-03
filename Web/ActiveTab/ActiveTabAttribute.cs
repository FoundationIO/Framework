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
