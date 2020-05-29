/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Infrastructure.Utils
{
    public static class StringUtils
    {
        private const string UpperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string NumberCharacters = "0123456789";
        private const string SpecialCharacters = "!@#$%&";

        private const string AlphanumericCharacters = UpperCaseCharacters + LowerCaseCharacters + NumberCharacters;
        private const string PasswordCharacters = AlphanumericCharacters + SpecialCharacters;

        public static string ToString(this List<string> obj, string sep = " ", bool useNewLine = true)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return ToString((IList<string>)obj.ToArray(), sep, useNewLine);
        }

        public static string ToString(this IList<string> obj, string sep = " ", bool useNewLine = true)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return ToString(obj.ToArray(), sep, useNewLine);
        }

        public static string ToString(this string[] obj, string sep = " ", bool useNewLine = true)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            var pos = 0;
            foreach (var item in obj)
            {
                if (useNewLine)
                {
                    sb.AppendLine(item + ((pos != obj.Length - 1) ? sep : string.Empty));
                }
                else
                {
                    sb.Append(item + ((pos != obj.Length - 1) ? sep : string.Empty));
                }

                pos++;
            }

            return sb.ToString();
        }

        public static string RemoveLastCharAndAddFirstChar(this string str, char citem)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            str = AddFirstChar(str, citem);
            str = RemoveLastChar(str, citem);
            return str;
        }

        public static string AddFirstChar(this string str, char citem)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            if (str.FirstChar() != citem.ToString())
            {
                str = citem.ToString() + str;
            }

            return str;
        }

        public static string RemoveLastChar(this string str, char citem)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            if (str.LastChar() == citem.ToString())
            {
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }

        public static string RemoveFirstChar(this string str, char citem)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            if (str.FirstChar() == citem.ToString())
            {
                str = str.Substring(1);
            }

            return str;
        }

        public static string FlattenString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            str = str.Replace('\n', ' ');
            str = str.Replace('\t', ' ');
            str = str.Replace('\r', ' ');
            return str;
        }

        public static string FirstChar(this string str)
        {
            if (str.Length > 0)
            {
                return str[0].ToString();
            }

            return string.Empty;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string FormatString(this string fmt, params object[] args)
        {
            return string.Format(fmt, args);
        }

        public static string LastChar(this string str)
        {
            if (str.Length > 0)
            {
                return str[str.Length - 1].ToString();
            }

            return string.Empty;
        }

        public static bool IsTrimmedStringNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            return string.IsNullOrEmpty(str.Trim());
        }

        public static bool IsTrimmedStringNotNullOrEmpty(this string str)
        {
            return !IsTrimmedStringNullOrEmpty(str);
        }

        public static bool IsSameWithCaseIgnoreTrimmed(this string str, string anotherStr)
        {
            if ((str == null) && (anotherStr == null))
            {
                return true;
            }

            if ((str == null) || (anotherStr == null))
            {
                return false;
            }

            return str.Trim().ToLower() == anotherStr.Trim().ToLower();
        }

        public static string GetRandomAlphanumericString(int length)
        {
            return GetRandomString(length, AlphanumericCharacters);
        }

        public static string GetSimplePasswordString(int upperCaseLength, int lowerCaseLength, int specialCharLength)
        {
            var upper = GetRandomString(upperCaseLength, UpperCaseCharacters);
            var lower = GetRandomString(lowerCaseLength, LowerCaseCharacters);
            var special = GetRandomString(specialCharLength, SpecialCharacters);
            return upper + lower + special;
        }

        public static string GetPasswordString(int upperCaseLength, int lowerCaseLength, int specialCharLength)
        {
            using (RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider())
            {
                var result = GetSimplePasswordString(upperCaseLength, lowerCaseLength, specialCharLength);
                return string.Concat(result.OrderBy(x => GetNextInt32(rnd)));
            }
        }

        public static string GetRandomString(int length)
        {
            return GetRandomString(length, PasswordCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            var result = new char[length];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(bytes);
            }

            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }

            return new string(result);
        }

        public static string MakeFirstCharAsLower(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            return str[0].ToString().ToLower() + str.Substring(1);
        }

        public static string MakeFirstCharAsUpper(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            return str[0].ToString().ToUpper() + str.Substring(1);
        }

        public static bool HasCharactersCount(this string s, int startLen, int endLen)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return (s.Length >= startLen) && (s.Length <= endLen);
        }

        public static bool HasUpperCaseLetters(this string s, int startLen)
        {
            return HasUpperCaseLetters(s, startLen, startLen);
        }

        public static bool HasUpperCaseLetters(this string s, int startLen, int endLen)
        {
            return ContainsCheckWithVaidationFunction(s, startLen, endLen, char.IsUpper);
        }

        public static bool HasLowerCaseLetters(this string s, int startLen)
        {
            return HasLowerCaseLetters(s, startLen, startLen);
        }

        public static bool HasLowerCaseLetters(this string s, int startLen, int endLen)
        {
            return ContainsCheckWithVaidationFunction(s, startLen, endLen, char.IsLower);
        }

        public static bool HasNumbers(this string s, int startLen, int endLen)
        {
            return ContainsCheckWithVaidationFunction(s, startLen, endLen, char.IsNumber);
        }

        public static string SubstringByChar(this string str, char separtor)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            var strIndex = str.IndexOf(separtor);

            if (strIndex == -1)
                return string.Empty;

            var index = strIndex + 1;

            return str.Substring(index);
        }

        public static string[] SplitByComma(string source)
        {
            var perms = source.Split(new char[1] { ',' });
            for (int i = 0; i < perms.Length; i++)
            {
                perms[i] = perms[i].Trim();
            }

            return perms;
        }

        public static string GetChainByLinkCount(string chain, int maxlink)
        {
            if (string.IsNullOrWhiteSpace(chain))
                return "";
            var buf = new StringBuilder();
            var slist = SplitByDelim(chain, '-');
            for (int i = 0; i < maxlink && slist.Length > i; i++)
            {
                if (i != 0)
                    buf.Append("-");
                buf.Append(slist[i]);
            }

            return buf.ToString();
        }

        public static string[] SplitByDelim(string source, char delim)
        {
            var perms = source.Split(new char[1] { delim });
            for (int i = 0; i < perms.Length; i++)
            {
                perms[i] = perms[i].Trim();
            }

            return perms;
        }

        public static string ConvertArrayToString(string[] source)
        {
            var buf = new StringBuilder(20);
            bool first = true;
            foreach (string s in source)
            {
                if (!first)
                    buf.Append(",");
                buf.Append(s);
                first = false;
            }

            return buf.ToString();
        }

        public static int[] SplitByCommaInt32(string source)
        {
            var values = source.Split(new char[1] { ',' });
            var lstIntValues = new List<int>();
            foreach (string v in values)
            {
                lstIntValues.Add(Convert.ToInt32(v));
            }

            return lstIntValues.ToArray();
        }

        public static string InitialCap(this string inStr)
        {
            if (inStr == null || inStr.Trim().Length == 0)
                return "";
            var lowerStr = inStr.ToLower();
            var str = lowerStr.Substring(0, 1).ToUpper();
            if (lowerStr.Length > 1)
                str += lowerStr.Substring(1);

            return str;
        }

        public static List<string> SplitOnSpace(this string text)
        {
            if (text == null)
                return new List<string>();
            return text.Split(' ').ToList();
        }

        public static string SplitCamelCase(this string input)
        {
            if (input == null)
                return "";
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        public static string FirstFewChars(this string item, int count)
        {
            if (item == null)
                return null;

            if (item.Length < count)
                return item;
            return item.Substring(0, count);
        }

        public static string LastFewChars(this string item, int count)
        {
            if (item == null)
                return null;

            if (item.Length < count)
                return item;

            var start = item.Length - count;

            return item.Substring(start);
        }

        private static bool ContainsCheckWithVaidationFunction(this string s, int startLen, int endLen, Func<char, bool> validationFunction)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            var count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (validationFunction(s[i]))
                    count++;
            }

            return (count >= startLen) && (count <= endLen);
        }

        private static int GetNextInt32(RNGCryptoServiceProvider rnd)
        {
            var randomInt = new byte[4];
            rnd.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }
    }
}
