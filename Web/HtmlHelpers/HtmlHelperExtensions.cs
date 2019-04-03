/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
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
            List<TextWithValue> items = new List<TextWithValue>
            {
                new TextWithValue { Text = "10", Value = "10" },
                new TextWithValue { Text = "25", Value = "25" },
                new TextWithValue { Text = "50", Value = "50" },
                new TextWithValue { Text = "100", Value = "100" },
                new TextWithValue { Text = "500", Value = "500" },
                new TextWithValue { Text = "1000", Value = "1000" }
            };
            return DropDownListEx(html, id, items, selectedItem, className, style, callbackFunction, isPositionRelative);
        }

        public static HtmlString DropDownListEx(this IHtmlHelper html, string id, List<TextWithValue> items, string selectedItem, string className, string style, string callbackFunction = null, bool isPositionRelative = false)
        {
            StringBuilder sb = new StringBuilder();
            string onChangeEvent = "";

            if (callbackFunction.IsTrimmedStringNotNullOrEmpty())
                onChangeEvent = $"onChange=\"javascript:{callbackFunction}(this);\"";

            sb.Append("<select id =\"").Append(id).Append("\" name=\"").Append(id).Append("\" class=\"").Append(className).Append("\" style =\"").Append(style).Append("\"  ").Append(onChangeEvent).AppendLine("  >");
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
