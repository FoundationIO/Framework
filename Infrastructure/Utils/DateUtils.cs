/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Utils
{
    public static class DateUtils
    {
        public static readonly DateTime InvalidDate = new DateTime(1900, 1, 1);

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
    }
}
