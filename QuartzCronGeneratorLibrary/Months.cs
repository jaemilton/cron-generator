using System;

namespace QuartzCronGenerator
{
    [Flags]
    public enum Months
    {
        January = 1,
        February = 2,
        March = 4,
        April = 8,
        May = 16,
        June = 32,
        July = 64,
        August = 128,
        September = 256,
        October = 512,
        November = 1024,
        December = 2048

        
    }


    static class MonthsHelper
    {
        public static Months GetfromMonthNumber(int monthNumber)
        {
            return (Months)Math.Pow(2, monthNumber -1);
        }
    }

}
