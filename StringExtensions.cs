using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalize string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this String s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                return char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static GroupCollection GrabAll(this String s, string pattern, bool ignoreCase = true)
        {
            Match match = (ignoreCase) ? Regex.Match(s, pattern, RegexOptions.IgnoreCase) : Regex.Match(s, pattern);
            return match.Groups;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string GrabFirst(this String s, string pattern, bool ignoreCase = true)
        {
            Match match = (ignoreCase) ? Regex.Match(s, pattern, RegexOptions.IgnoreCase) : Regex.Match(s, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Matches(this String s, string pattern, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                return ((Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase))) ? true : false;
            }
            else
            {
                return (Regex.IsMatch(s, pattern)) ? true : false;
            }
        }

        public static string Take(this string theString, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(0, lengthToTake);

            if (ellipsis && lengthToTake < theString.Length)
                cutDownString += "...";

            return cutDownString;
        }

        //like linq skip - skips the first x characters and returns the remaining string
        public static string Skip(this string theString, int count)
        {
            int startIndex = Math.Min(count, theString.Length);
            int remainingLength = theString.Length - startIndex;

            var cutDownString = theString.Substring(startIndex - 1, remainingLength);

            return cutDownString;
        }

        /// <summary>
        //reverses the string... pretty obvious really
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        // "a string".IsNullOrEmpty() beats string.IsNullOrEmpty("a string")
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        /// <summary>
        //not so sure about this one -
        //"a string {0}".Format("blah") vs string.Format("a string {0}", "blah") 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        //splits string into array with chunks of given size. not really that useful.. 
        /// </summary>
        /// <param name="toSplit"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static string[] SplitIntoChunks(this string toSplit, int chunkSize)
        {
            if (string.IsNullOrEmpty(toSplit))
                return new string[] { "" };

            int stringLength = toSplit.Length;

            int chunksRequired = (int)Math.Ceiling((decimal)stringLength / (decimal)chunkSize);
            var stringArray = new string[chunksRequired];

            int lengthRemaining = stringLength;

            for (int i = 0; i < chunksRequired; i++)
            {
                int lengthToUse = Math.Min(lengthRemaining, chunkSize);
                int startIndex = chunkSize * i;
                stringArray[i] = toSplit.Substring(startIndex, lengthToUse);

                lengthRemaining = lengthRemaining - lengthToUse;
            }

            return stringArray;
        }

        /// <summary>
        /// Returns true if invoking string matches with regex pattern
        /// </summary>
        /// <param name="original"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsRegexMatch(this string original, string regex)
        {
            return Regex.IsMatch(original, regex);
        }

        /// <summary>
        /// Indicates whether invoking string object is not null and not an empty string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool IsNotNullAndNotEmpty(this string inputString)
        {
            return !string.IsNullOrEmpty(inputString);
        }

        /// <summary>
        /// Returns an empty string if input string is null.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string IfNullThenEmpty(this string inputString)
        {
            return inputString ?? string.Empty;
        }

        public static bool IsInt32(this string s)
        {
            int tmp;
            if (Int32.TryParse(s, out tmp))
            {
                return true;
            }

            return false;
        }

        public static bool IsNotInt32(this string s)
        {
            return !IsInt32(s);
        }

        public static bool IsMatch(this string s, string pattern)
        {
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// Replaces all spaces in <c>s</c> with the hyphen "-"
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string Hyphenate(this string s)
        {
            return Regex.Replace(s, @"(\s+|%20|\+)", "-");
        }

        /// <summary>
        /// Unhyphenates the specified string <c>s</c> by replacing all hyphens
        /// with a single space.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string Unhyphenate(this string s)
        {
            return Regex.Replace(s, @"-", " ");
        }

        /// <summary>
        /// count Number of words
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Checks string object's value to array of string values
        /// </summary>        
        /// <param name="stringValues">Array of string values to compare</param>
        /// <returns>Return true if any string value matches</returns>
        public static bool In(this string value, params string[] stringValues)
        {
            foreach (string otherValue in stringValues)
                if (string.Compare(value, otherValue) == 0)
                    return true;

            return false;
        }

        /// <summary>
        /// Converts string to enum object
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from left</returns>
        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }

        /// <summary>
        ///  Replaces the format item in a specified System.String with the text equivalent
        ///  of the value of a specified System.Object instance.
        /// </summary>
        /// <param name="value">A composite format string</param>
        /// <param name="arg0">An System.Object to format</param>
        /// <returns>A copy of format in which the first format item has been replaced by the
        /// System.String equivalent of arg0</returns>
        public static string Format(this string value, object arg0)
        {
            return string.Format(value, arg0);
        }

        /// <summary>
        ///  Replaces the format item in a specified System.String with the text equivalent
        ///  of the value of a specified System.Object instance.
        /// </summary>
        /// <param name="value">A composite format string</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String
        /// equivalent of the corresponding instances of System.Object in args.</returns>
        public static string FormatArgs(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        // <summary>
        /// Formats the string according to the specified mask
        /// https://www.extensionmethod.net/csharp/string/formatwithmask
        /// var s = "aaaaaaaabbbbccccddddeeeeeeeeeeee".FormatWithMask("Hello ########-#A###-####-####-############ Oww");
        //  s.ShouldEqual("Hello aaaaaaaa-bAbbb-cccc-dddd-eeeeeeeeeeee Oww");
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
        /// <returns>The formatted string</returns>
        public static string FormatWithMask(this string input, string mask)
        {
            if (input.IsNullOrEmpty()) return input;
            var output = string.Empty;
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output += input[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }

        /// <summary>
        /// Checks if a string value is numeric according to you system culture
        /// https://www.extensionmethod.net/csharp/string/isnumeric-2
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        /// <summary>
        /// Used when we want to completely remove HTML code and not encode it with XML entities
        /// https://www.extensionmethod.net/csharp/string/strip-html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        /// <summary>
        /// UpperCase first Character of string
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string UcFirst(this string theString)
        {
            if (string.IsNullOrEmpty(theString))
            {
                return string.Empty;
            }

            char[] theChars = theString.ToCharArray();
            theChars[0] = char.ToUpper(theChars[0]);

            return new string(theChars);

        }

        /// <summary>
        /// Converts a string into a "SecureString"
        /// https://www.extensionmethod.net/csharp/string/tosecurestring
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static System.Security.SecureString ToSecureString(this String str)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            foreach (Char c in str)
                secureString.AppendChar(c);

            return secureString;
        }

        /// <summary>
        /// Convert a String into CamelCase
        /// https://www.extensionmethod.net/csharp/string/tocamelcase
        /// </summary>
        /// <param name="the_string"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string the_string)
        {
            if (the_string == null || the_string.Length < 2)
                return the_string;

            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
    }
}
