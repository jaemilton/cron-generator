﻿using QuartzCronGenerator;
using System.Collections.Generic;
using Xunit;

namespace QuartzCronGeneratorLibrary.Tests
{
    public class Tests
    {
        [Fact]
        public void TestEveryNSeconds()
        {
            var ce1 = QuartzCronExpression.EveryNSeconds(1);
            var ce2 = QuartzCronExpression.EveryNSeconds(59);
            var ce3 = QuartzCronExpression.EveryNSeconds(3600);

            Assert.Equal("0/1 * * 1/1 * ? *", ce1);
            Assert.Equal("0/59 * * 1/1 * ? *", ce2);
            Assert.Equal("0/3600 * * 1/1 * ? *", ce3);
        }

        [Fact]
        public void TestEveryNMinutes()
        {
            var ce1 = QuartzCronExpression.EveryNMinutes(1);
            var ce2 = QuartzCronExpression.EveryNMinutes(59);
            var ce3 = QuartzCronExpression.EveryNMinutes(3600);

            Assert.Equal("0 0/1 * 1/1 * ? *", ce1);
            Assert.Equal("0 0/59 * 1/1 * ? *", ce2);
            Assert.Equal("0 0/3600 * 1/1 * ? *", ce3);
        }

        [Fact]
        public void TestEveryNHours()
        {
            var ce1 = QuartzCronExpression.EveryNHours(1);
            var ce2 = QuartzCronExpression.EveryNHours(23);
            var ce3 = QuartzCronExpression.EveryNHours(72);

            Assert.Equal("0 0 0/1 1/1 * ? *", ce1);
            Assert.Equal("0 0 0/23 1/1 * ? *", ce2);
            Assert.Equal("0 0 0/72 1/1 * ? *", ce3);
        }

        [Fact]
        public void TestEveryDayAt()
        {
            var ce1 = QuartzCronExpression.EveryDayAt(12, 0);
            var ce2 = QuartzCronExpression.EveryDayAt(7, 23);
            var ce3 = QuartzCronExpression.EveryDayAt(22, 22);

            Assert.Equal("0 0 12 1/1 * ? *", ce1);
            Assert.Equal("0 23 7 1/1 * ? *", ce2);
            Assert.Equal("0 22 22 1/1 * ? *", ce3);
        }

        [Fact]
        public void TestEveryNDaysAt()
        {
            var ce1 = QuartzCronExpression.EveryNDaysAt(1, 12, 0);
            var ce2 = QuartzCronExpression.EveryNDaysAt(6, 7, 23);
            var ce3 = QuartzCronExpression.EveryNDaysAt(30, 22, 22);

            Assert.Equal("0 0 12 1/1 * ? *", ce1);
            Assert.Equal("0 23 7 1/6 * ? *", ce2);
            Assert.Equal("0 22 22 1/30 * ? *", ce3);
        }

        [Fact]
        public void TestEveryWeekDay()
        {
            var ce1 = QuartzCronExpression.EveryWeekDayAt(12, 0);
            var ce2 = QuartzCronExpression.EveryWeekDayAt(7, 23);
            var ce3 = QuartzCronExpression.EveryWeekDayAt(22, 22);

            Assert.Equal("0 0 12 ? * MON-FRI *", ce1);
            Assert.Equal("0 23 7 ? * MON-FRI *", ce2);
            Assert.Equal("0 22 22 ? * MON-FRI *", ce3);
        }

        [Fact]
        public void TestEverySpecificWeekDayAt()
        {
            var ce1 = QuartzCronExpression.EverySpecificWeekDayAt(12, 0, DaysOfWeek.Monday);
            var ce2 = QuartzCronExpression.EverySpecificWeekDayAt(7, 23, DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday);
            var ce3 = QuartzCronExpression.EverySpecificWeekDayAt(22, 22, DaysOfWeek.Saturday | DaysOfWeek.Sunday);
            var ce4 = QuartzCronExpression.EverySpecificWeekDayAt(5, 20, DaysOfWeek.Thursday | DaysOfWeek.Tuesday);

            Assert.Equal("0 0 12 ? * MON *", ce1);
            Assert.Equal("0 23 7 ? * MON,WED,FRI *", ce2);
            Assert.Equal("0 22 22 ? * SUN,SAT *", ce3);
            Assert.Equal("0 20 5 ? * TUE,THU *", ce4);
        }

        [Fact]
        public void TestDaysOfWeekRepresentation()
        {
            var monString = CronConverter.ToCronRepresentationSingle(DaysOfWeek.Monday);
            Assert.Equal("MON", monString);

            const DaysOfWeek days = DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday;
            var daysList = new List<DaysOfWeek> { DaysOfWeek.Monday, DaysOfWeek.Wednesday, DaysOfWeek.Friday };
            Assert.Equal(daysList, CronConverter.GetFlags(days));

            const string expectedString = "MON,WED,FRI";
            Assert.Equal(expectedString, CronConverter.ToCronRepresentation(days));

            const string exprectedString2 = "SUN,SAT";
            Assert.Equal(exprectedString2, CronConverter.ToCronRepresentation(DaysOfWeek.Saturday | DaysOfWeek.Sunday));
        }

        [Fact]
        public void TestEverySpecificDayEveryNMonthAt()
        {
            var ce1 = QuartzCronExpression.EverySpecificDayEveryNMonthAt(1, 1, 12, 0);
            var ce2 = QuartzCronExpression.EverySpecificDayEveryNMonthAt(7, 3, 7, 15);
            var ce3 = QuartzCronExpression.EverySpecificDayEveryNMonthAt(28, 6, 21, 30);

            Assert.Equal("0 0 12 1 1/1 ? *", ce1);
            Assert.Equal("0 15 7 7 1/3 ? *", ce2);
            Assert.Equal("0 30 21 28 1/6 ? *", ce3);
        }

        [Fact]
        public void TestEverySpecificSeqWeekDayEveryNMonthAt()
        {
            var ce1 = QuartzCronExpression.EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber.First, DaysOfWeek.Monday, 1, 12, 0);
            var ce2 = QuartzCronExpression.EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber.Second, DaysOfWeek.Wednesday, 3, 7, 15);
            var ce3 = QuartzCronExpression.EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber.Third, DaysOfWeek.Friday, 6, 21, 30);
            var ce4 = QuartzCronExpression.EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber.Fourth, DaysOfWeek.Sunday, 77, 22, 45);

            Assert.Equal("0 0 12 ? 1/1 MON#1 *", ce1);
            Assert.Equal("0 15 7 ? 1/3 WED#2 *", ce2);
            Assert.Equal("0 30 21 ? 1/6 FRI#3 *", ce3);
            Assert.Equal("0 45 22 ? 1/77 SUN#4 *", ce4);
        }

  
        [Fact]
        public void TestEverySpecificSeqWeekDayOfMonthAt()
        {
            var ce1 = QuartzCronExpression.EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber.First, DaysOfWeek.Monday, Months.January, 12, 0);
            var ce2 = QuartzCronExpression.EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber.Second, DaysOfWeek.Wednesday, Months.February, 7, 15);
            var ce3 = QuartzCronExpression.EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber.Third, DaysOfWeek.Friday, Months.August, 21, 30);
            var ce4 = QuartzCronExpression.EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber.Fourth, DaysOfWeek.Sunday, Months.December, 22, 45);

            Assert.Equal("0 0 12 ? JAN MON#1 *", ce1);
            Assert.Equal("0 15 7 ? FEB WED#2 *", ce2);
            Assert.Equal("0 30 21 ? AUG FRI#3 *", ce3);
            Assert.Equal("0 45 22 ? DEC SUN#4 *", ce4);
        }


        [Fact]
        public void TestMothsRepresentation()
        {
            var monString = CronConverter.ToCronRepresentationSingle(Months.June);
            Assert.Equal("JUN", monString);

            const Months months = Months.January | Months.March | Months.June;
            var monthsList = new List<Months> { Months.January , Months.March , Months.June };
            Assert.Equal(monthsList, CronConverter.GetFlags(months));

            const string expectedString = "JAN,MAR,JUN";
            Assert.Equal(expectedString, CronConverter.ToCronRepresentation(months));

            const string exprectedString2 = "JAN,DEC";
            Assert.Equal(exprectedString2, CronConverter.ToCronRepresentation(Months.December | Months.January));
        }


        [Fact]
        public void TestEverySpecificDayOfMonthAt()
        {
            var ce1 = QuartzCronExpression.EverySpecificDayOfMonthAt(Months.April, 1, 10, 0);
            var ce2 = QuartzCronExpression.EverySpecificDayOfMonthAt(Months.January | Months.July, 3, 3, 0);
            var ce3 = QuartzCronExpression.EverySpecificDayOfMonthAt(Months.December | Months.January, 6, 1, 10);
            var ce4 = QuartzCronExpression.EverySpecificDayOfMonthAt(Months.January | Months.March | Months.June | Months.September, 25, 23, 0);

            Assert.Equal("0 0 10 1 APR ? *", ce1);
            Assert.Equal("0 0 3 3 JAN,JUL ? *", ce2);
            Assert.Equal("0 10 1 6 JAN,DEC ? *", ce3);
            Assert.Equal("0 0 23 25 JAN,MAR,JUN,SEP ? *", ce4);
        }

        [Fact]
        public void TestSpecificDateAt()
        {
            var ce1 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2020, 1, 1, 8, 30, 0));
            var ce2 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2019, 2, 1, 0, 0, 0));
            var ce3 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2022, 3, 27, 9, 0, 0));
            var ce4 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2015, 4, 1, 7, 0, 30));
            var ce5 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2020, 5, 1, 8, 30, 0));
            var ce6 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2019, 6, 1, 0, 0, 0));
            var ce7 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2022, 7, 27, 9, 0, 0));
            var ce8 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2015, 8, 1, 7, 0, 30));
            var ce9 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2020, 9, 1, 8, 30, 0));
            var ce10 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2019, 10, 1, 0, 0, 0));
            var ce11 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2022, 11, 27, 9, 0, 0));
            var ce12 = QuartzCronExpression.SpecificDateAt(new System.DateTime(2015, 12, 1, 7, 0, 30));

            Assert.Equal("0 30 8 1 JAN ? 2020", ce1);
            Assert.Equal("0 0 0 1 FEB ? 2019", ce2);
            Assert.Equal("0 0 9 27 MAR ? 2022", ce3);
            Assert.Equal("30 0 7 1 APR ? 2015", ce4);
            Assert.Equal("0 30 8 1 MAY ? 2020", ce5);
            Assert.Equal("0 0 0 1 JUN ? 2019", ce6);
            Assert.Equal("0 0 9 27 JUL ? 2022", ce7);
            Assert.Equal("30 0 7 1 AUG ? 2015", ce8);
            Assert.Equal("0 30 8 1 SEP ? 2020", ce9);
            Assert.Equal("0 0 0 1 OCT ? 2019", ce10);
            Assert.Equal("0 0 9 27 NOV ? 2022", ce11);
            Assert.Equal("30 0 7 1 DEC ? 2015", ce12);
        }
    }
}
