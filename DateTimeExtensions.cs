using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Wraps DateTime.TryParse() and all the other kinds of code you need to determine if a given string holds a value that can be converted into a DateTime object.
        /// https://www.extensionmethod.net/csharp/string/isdate
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool</returns>
        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return from p in range
                   select self.Date.AddDays(p);
        }
    }
}
