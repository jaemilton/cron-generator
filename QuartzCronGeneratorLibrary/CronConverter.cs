using System;
using System.Collections.Generic;
using System.Linq;

namespace QuartzCronGenerator
{
    public class CronConverter
    {
        public static string ToCronRepresentationSingle(DaysOfWeek day)
        {
            switch (day)
            {
                case DaysOfWeek.Monday:
                    return "MON";
                case DaysOfWeek.Tuesday:
                    return "TUE";
                case DaysOfWeek.Wednesday:
                    return "WED";
                case DaysOfWeek.Thursday:
                    return "THU";
                case DaysOfWeek.Friday:
                    return "FRI";
                case DaysOfWeek.Saturday:
                    return "SAT";
                case DaysOfWeek.Sunday:
                    return "SUN";
                default:
                    throw new ArgumentException();
            }
        }


        public static string ToCronRepresentationSingle(Months month)
        {
            switch (month)
            {
                case Months.January:
                    return "JAN";
                case Months.February:
                    return "FEB";
                case Months.March:
                    return "MAR";
                case Months.April:
                    return "APR";
                case Months.May:
                    return "MAY";
                case Months.June:
                    return "JUN";
                case Months.July:
                    return "JUL";
                case Months.August:
                    return "AUG";
                case Months.September:
                    return "SEP";
                case Months.October:
                    return "OCT";
                case Months.November:
                    return "NOV";
                case Months.December:
                    return "DEC";
                default:
                    throw new ArgumentException();
            }
            
        }

        /// <summary>
        /// Converts enumerator DaysOfWeek into string representation
        /// like "MON, TUE, WED"
        /// </summary>
        /// <param name="days">Enumerator to convert</param>
        /// <returns>String representation</returns>
        public static string ToCronRepresentation(DaysOfWeek days)
        {
            return String.Join(",", GetFlags(days).Select(ToCronRepresentationSingle));
        }

        public static IEnumerable<DaysOfWeek> GetFlags(DaysOfWeek days)
        {
            return Enum.GetValues(days.GetType()).Cast<DaysOfWeek>().Where(v => days.HasFlag(v));
        }


        /// <summary>
        /// Converts enumerator Months into string representation
        /// like "JAN, FEB, MAR"
        /// </summary>
        /// <param name="days">Enumerator to convert</param>
        /// <returns>String representation</returns>
        public static string ToCronRepresentation(Months months)
        {
            return String.Join(",", GetFlags(months).Select(ToCronRepresentationSingle));
        }

        public static IEnumerable<Months> GetFlags(Months months)
        {
            return Enum.GetValues(months.GetType()).Cast<Months>().Where(v => months.HasFlag(v));
        }
    }
}
