/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Text;

namespace Framework.Infrastructure.Utils
{
    public static class DateUtils
    {
        public const int SECOND = 1;
        public const int MINUTE = 60 * SECOND;
        public const int HOUR = 60 * MINUTE;
        public const int DAY = 24 * HOUR;
        public const int MONTH = 30 * DAY;

        public static readonly DateTime InvalidDate = new DateTime(1900, 1, 1);
        public static readonly DateTime PastDate = new DateTime(1977, 1, 23);

        public static bool IsInvalidDate(this DateTime? dt)
        {
            return (dt == null) || (dt == InvalidDate) || (dt == DateTime.MinValue) || (dt == DateTime.MaxValue);
        }

        public static bool IsInvalidDate(this DateTime dt)
        {
            return (dt == InvalidDate) || (dt == DateTime.MinValue) || (dt == DateTime.MaxValue);
        }

        public static bool IsValidDate(this DateTime? dt)
        {
            return !IsInvalidDate(dt);
        }

        public static bool IsValidDate(this DateTime dt)
        {
            return !IsInvalidDate(dt);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime UpdateDatePart(this DateTime date, DateTime newDatePart)
        {
            return new DateTime(newDatePart.Year, newDatePart.Month, newDatePart.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public static DateTime UpdateDatePart(this DateTime date, int year, int month, int day)
        {
            return new DateTime(year, month, day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public static string ToReadableString(this TimeSpan span)
        {
            var dur = span.Duration();
            var sb = new StringBuilder();
            if (dur.Days > 0)
                sb.AppendFormat("{0:0} day{1}, ", span.Days, span.Days == 1 ? "" : "s");
            if (dur.Hours > 0)
                sb.AppendFormat("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? "" : "s");
            if (dur.Minutes > 0)
                sb.AppendFormat("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? "" : "s");
            if (dur.Seconds > 0)
                sb.AppendFormat("{0:0} second{1}, ", span.Seconds, span.Seconds == 1 ? "" : "s");
            if (sb.Length >= 2)
                sb.Length -= 2;

            var result = sb.ToString();
            if (result.IsNullOrEmpty())
                return "0 seconds";
            else
                return result;
        }

        public static string FriendlyRelativeTime(this DateTime date)
        {
            if (date > DateTime.UtcNow)
            {
                return "in the future";
            }

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            if (delta < 0)
            {
                return "not yet";
            }

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }

            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }

            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }

            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }

            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours ago";
            }

            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }

            if (delta < 30 * DAY)
            {
                return ts.Days + " days ago";
            }

            if (delta < 12 * MONTH)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}
