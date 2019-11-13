/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Utils
{
    public static class SafeUtils
    {
        public static int Int(string obj, int defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                if (int.TryParse(obj, out int result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static bool Bool(string obj, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }

            string bstr = obj.Trim().ToUpper();
            if ((bstr == "ON") || (bstr == "T") || (bstr == "TRUE") || (bstr == "Y") || (bstr == "YES") || (bstr == "1") || (Int(bstr) > 0))
            {
                return true;
            }

            return false;
        }

        public static bool Bool(object obj, bool defaultValue = false)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                if (obj is string s)
                {
                    return Bool(s, defaultValue);
                }

                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static ushort UShort(string obj, ushort defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                if (ushort.TryParse(obj, out ushort result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static short Short(object obj, short defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static long Long(object obj, long defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static decimal Decimal(string obj, decimal defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                if (decimal.TryParse(obj, out decimal result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float Float(object obj, float defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToSingle(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double Double(object obj, double defaultValue = 0.0)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T Enum<T>(string enumString, object defaultValue)
        {
            T enumValue = default;

            if (!string.IsNullOrEmpty(enumString))
            {
                try
                {
                    enumValue = (T)System.Enum.Parse(typeof(T), enumString);
                }
                catch (Exception)
                {
                    if (defaultValue != null)
                    {
                        enumValue = (T)System.Enum.ToObject(typeof(T), defaultValue);
                    }
                }
            }
            else
            {
                if (defaultValue != null)
                {
                    enumValue = (T)System.Enum.ToObject(typeof(T), defaultValue);
                }
            }

            return enumValue;
        }

        public static T Enum<T>(int enumValue, object defaultValue)
        {
            T enumRet;
            try
            {
                enumRet = (T)System.Enum.ToObject(typeof(T), enumValue);
            }
            catch (Exception)
            {
                enumRet = (T)System.Enum.ToObject(typeof(T), defaultValue);
            }

            return enumRet;
        }

        public static object Enum(Type enumType, string enumString, object defaultValue)
        {
            object enumValue = null;

            if (!string.IsNullOrEmpty(enumString))
            {
                try
                {
                    enumValue = System.Enum.Parse(enumType, enumString);
                }
                catch (Exception)
                {
                    if (defaultValue != null)
                    {
                        enumValue = System.Enum.ToObject(enumType, defaultValue);
                    }
                }
            }
            else
            {
                if (defaultValue != null)
                {
                    enumValue = System.Enum.ToObject(enumType, defaultValue);
                }
            }

            return enumValue;
        }

        public static object Enum(Type enumType, int enumValue, object defaultValue)
        {
            try
            {
                return System.Enum.ToObject(enumType, enumValue);
            }
            catch (Exception)
            {
                return System.Enum.ToObject(enumType, defaultValue);
            }
        }

        public static Guid Guid(string value, System.Guid defaultValue)
        {
            try
            {
                if (System.Guid.TryParse(value, out Guid result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Guid Guid(string value)
        {
            return Guid(value, System.Guid.NewGuid());
        }

        public static DateTime DateTime(string obj, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }

            try
            {
                if (System.DateTime.TryParse(obj, out DateTime result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime DateTime(DateTime? obj, DateTime defaultValue)
        {
            if (!DateUtils.IsValidDate(obj))
            {
                return defaultValue;
            }

            try
            {
                return obj.Value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime DateTime(string obj)
        {
            return DateTime(obj, System.DateTime.MinValue);
        }
    }
}
