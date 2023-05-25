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
    
     public static class DateTimeExtension
    {
        public static bool IsTodayOrInFuture(this DateTime date, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return (date.Date == now.Date || date > now) && date < endDate;
        }

        public static bool IsToday(this DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        public static bool IsGreaterThan(this DateTime date, DateTime other)
        {
            return date > other;
        }

        public static bool IsLessThan(this DateTime date, DateTime other)
        {
            return date < other;
        }

        public static DateTime StartOfNextMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1);
        }

        public static IEnumerable<DateTime> Next36Months(this DateTime date)
        {
            DateTime nextDate = date.AddMonths(1).AddDays(-date.Day + 1);
            for (int i = 0; i < 36; i++)
            {
                yield return nextDate;
                nextDate = nextDate.AddMonths(1);
            }
        }
        public static DateTime Get36MonthDate(this DateTime date)
        {
            return date.AddMonths(36);
        }
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }
    }
}
