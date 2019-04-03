using Framework.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Framework.Web.ActiveTab
{
    public static class ActiveTabHelper
    {
        private const string ActiveDataViewDataName = "ActiveTabId";

        public static bool IsActiveTab(this IHtmlHelper html,int tabNumber)
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
