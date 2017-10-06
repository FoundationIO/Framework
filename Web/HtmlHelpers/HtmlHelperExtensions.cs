using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Infrastructure.Models;
using Framework.Infrastructure.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Framework.Web.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString PageDropDownList(this IHtmlHelper html, string id, string selectedItem, string className, string style, string callbackFunction = null, bool isPositionRelative = false)
        {
            List<TextWithValue> items = new List<TextWithValue>();
            items.Add(new TextWithValue { Text = "10", Value = "10" });
            items.Add(new TextWithValue { Text = "25", Value = "25" });
            items.Add(new TextWithValue { Text = "50", Value = "50" });
            items.Add(new TextWithValue { Text = "100", Value = "100" });
            items.Add(new TextWithValue { Text = "500", Value = "500" });
            items.Add(new TextWithValue { Text = "1000", Value = "1000" });
            return DropDownListEx(html, id, items, selectedItem, className, style, callbackFunction, isPositionRelative);
        }

        public static HtmlString DropDownListEx(this IHtmlHelper html, string id, List<TextWithValue> items, string selectedItem, string className, string style, string callbackFunction = null, bool isPositionRelative = false)
        {
            StringBuilder sb = new StringBuilder();
            var defaultText = string.Empty;
            var defaultValue = string.Empty;
            StringBuilder sbMenu = new StringBuilder();
            string onChangeEvent = "";

            if (callbackFunction.IsTrimmedStringNotNullOrEmpty())
                onChangeEvent = $"onChange=\"javascript:{callbackFunction}(this);\"";

            sb.AppendLine($"<select id =\"{id}\" name=\"{id}\" class=\"{className}\" style =\"{style}\"  {onChangeEvent}  >");
            foreach (var listItem in items)
            {
                var selected = string.Empty;
                if (selectedItem != null && listItem.Value == selectedItem.ToString())
                {
                    selected = " selected ";
                }

                sb.AppendLine($"<option value=\"{listItem.Value}\"{selected} >{listItem.Text}</option>");
            }

            sb.AppendLine("</select>");
            return new HtmlString(sb.ToString());
        }
    }
}
