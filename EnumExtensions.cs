using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if the target enumeration includes the specified flag.
        /// </summary>
        /// <remarks>
        /// From http://www.codeproject.com/KB/cs/fun-with-cs-extensions.aspx?msg=2838918#xx2838918xx
        /// </remarks>
        /// <param name="target">The target.</param>
        /// <param name="flags">The flags.</param>
        /// <returns><b>true</b> if match, otherwise <b>false</b>.</returns>
        public static bool Includes<TEnum>(this TEnum target, TEnum flags) where TEnum : IComparable, IConvertible, IFormattable
        {
            if (target.GetType() != flags.GetType())
            {
                throw new ArgumentException("Enum type mismatch", "flags");
            }

            long a = Convert.ToInt64(target);
            long b = Convert.ToInt64(flags);
            return (a & b) == b;
        }

        /// <summary>
        /// Checks if the target enumeration includes any of the specified flags
        /// </summary>
        /// <remarks>
        /// From http://www.codeproject.com/KB/cs/fun-with-cs-extensions.aspx?msg=2838918#xx2838918xx
        /// </remarks>
        /// <param name="target">The target.</param>
        /// <param name="flags">The flags.</param>
        /// <returns><b>true</b> if match, otherwise <b>false</b>.</returns>
        public static bool IncludesAny<TEnum>(this TEnum target, TEnum flags) where TEnum : IComparable, IConvertible, IFormattable
        {
            if (target.GetType() != flags.GetType())
            {
                throw new ArgumentException("Enum type mismatch", "flags");
            }

            long a = Convert.ToInt64(target);
            long b = Convert.ToInt64(flags);
            return (a & b) != 0L;
        }

        public static string ToDescriptionString(this Enum val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    /// <summary>
    /// https://www.extensionmethod.net/csharp/enum/enum-t-parse-and-enum-t-tryparse
    /// Parses the specified string value into the Enum type passed. Also contains a bool to determine whether or not the case should be ignored.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Enum<T>
    {

        public static T Parse(string value)
        {
            return Enum<T>.Parse(value, true);
        }

        public static T Parse(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParse(string value, out T returnedValue)
        {
            return Enum<T>.TryParse(value, true, out returnedValue);
        }

        public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
                return true;
            }
            catch
            {
                returnedValue = default(T);
                return false;
            }
        }
    }
}
