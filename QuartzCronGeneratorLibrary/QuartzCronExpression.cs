using System;

namespace QuartzCronGenerator
{
    public class QuartzCronExpression
    {
        private readonly DaysOfWeek _daysOfWeek;
        private readonly Months _months;
        private readonly int _interval;
        private readonly DaySeqNumber _daySeqNumber;
        private readonly int _dayNumber;
        private readonly int _startHour;
        private readonly int _startMinute;
        private readonly int _yearNumber;
        private readonly int _startSecond;

        private readonly CronExpressionType _expressionType;
        private string _cronString;

        public CronExpressionType ExpressionType
        {
            get { return _expressionType; }
        }

        private void BuildCronExpression()
        {
            switch (ExpressionType)
            {
                case CronExpressionType.EveryNSeconds:
                    _cronString = string.Format("0/{0} * * 1/1 * ? *", _interval);
                    break;
                case CronExpressionType.EveryNMinutes:
                    _cronString = $"0 0/{_interval} * 1/1 * ? *";
                    break;
                case CronExpressionType.EveryNHours:
                    _cronString = $"0 0 0/{_interval} 1/1 * ? *";
                    break;
                case CronExpressionType.EveryNDaysAt:
                case CronExpressionType.EveryDayAt:
                    _cronString = $"0 {_startMinute} {_startHour} 1/{_interval} * ? *";
                    break;
                case CronExpressionType.EveryWeekDay:
                    _cronString = $"0 {_startMinute} {_startHour} ? * MON-FRI *";
                    break;
                case CronExpressionType.EverySpecificWeekDayAt:
                    _cronString = $"0 {_startMinute} {_startHour} ? * {CronConverter.ToCronRepresentation(_daysOfWeek)} *";
                    break;
                case CronExpressionType.EverySpecificDayEveryNMonthAt:
                    _cronString = $"0 {_startMinute} {_startHour} {_dayNumber} 1/{_interval} ? *";
                    break;
                case CronExpressionType.EverySpecificSeqWeekDayEveryNMonthAt:
                    _cronString = $"0 {_startMinute} {_startHour} ? 1/{_interval} {CronConverter.ToCronRepresentationSingle(_daysOfWeek)}#{(int)_daySeqNumber} *";
                    break;
                case CronExpressionType.EverySpecificDayOfMonthAt:
                    _cronString = $"0 {_startMinute} {_startHour} {(int)_daySeqNumber} {(int)_months} ? *";
                    break;
                case CronExpressionType.EverySpecificSeqWeekDayOfMonthAt:
                    _cronString = $"0 {_startMinute} {_startHour} ? {(int)_months} {CronConverter.ToCronRepresentationSingle(_daysOfWeek)}#{(int)_daySeqNumber} *";
                    break;
                case CronExpressionType.EverySpecificDayOfSpecificMonthAt:
                    _cronString = $"0 {_startMinute} {_startHour} {_dayNumber} {CronConverter.ToCronRepresentation(_months)} ? *";
                    break;
                case CronExpressionType.SpecificDateAt:
                    _cronString = $"{_startSecond} {_startMinute} {_startHour} {_dayNumber} {(int)_months} ? {_yearNumber}";
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        #region "Protected Contructors"
        protected QuartzCronExpression(int interval, CronExpressionType expressionType)
        {
            _interval = interval;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(int startHour, int startMinute, CronExpressionType expressionType)
        {
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(int interval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _interval = interval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(DaysOfWeek daysOfWeek, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _daysOfWeek = daysOfWeek;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(int dayNumber, int interval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = dayNumber;
            _interval = interval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(DaySeqNumber daySeqNumber, DaysOfWeek daysOfWeek, int monthInverval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _daySeqNumber = daySeqNumber;
            _daysOfWeek = daysOfWeek;
            _interval = monthInverval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(Months month, int dayNumber, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _months = month;
            _dayNumber = dayNumber;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(DaySeqNumber dayNumber, DaysOfWeek daysOfWeek, Months month, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _daySeqNumber = dayNumber;
            _daysOfWeek = daysOfWeek;
            _months = month;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(int dayNumber, Months months, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = dayNumber;
            _months = months;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected QuartzCronExpression(int secondNumber, int startMinute, int startHour, int dayNumber, int monthNumber, int yearNumber, CronExpressionType expressionType)
        {
            _startSecond = secondNumber;
            _startMinute = startMinute;
            _startHour = startHour;
            _dayNumber = dayNumber;
            _months = (Months)monthNumber;
            _yearNumber = yearNumber;
            _expressionType = expressionType;

            BuildCronExpression();
        }
        #endregion "Protected Contructors"

        /// <summary>
        /// Create new CronExpression instance, which occurs every *secondInteval* seconds
        /// </summary>
        /// <param name="secondsInteval">Interval in seconds</param>
        /// <returns></returns>
        public static QuartzCronExpression EveryNSeconds(int secondsInteval)
        {
            var ce = new QuartzCronExpression(secondsInteval, CronExpressionType.EveryNSeconds);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every *minutesInteval* minutes
        /// </summary>
        /// <param name="minutesInteval">Interval in minutes</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EveryNMinutes(int minutesInteval)
        {
            var ce = new QuartzCronExpression(minutesInteval, CronExpressionType.EveryNMinutes);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every *hoursInterval* hours
        /// </summary>
        /// <param name="hoursInterval">Interval in hours</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EveryNHours(int hoursInterval)
        {
            var ce = new QuartzCronExpression(hoursInterval, CronExpressionType.EveryNHours);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every day at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EveryDayAt(int hour, int minute)
        {
            var ce = new QuartzCronExpression(1, hour, minute, CronExpressionType.EveryDayAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every *daysInterval* days at specified hours
        /// </summary>
        /// <param name="daysInterval">Interval in days</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EveryNDaysAt(int daysInterval, int hour, int minute)
        {
            var ce = new QuartzCronExpression(daysInterval, hour, minute, CronExpressionType.EveryNDaysAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs monday to friday at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EveryWeekDayAt(int hour, int minute)
        {
            var ce = new QuartzCronExpression(hour, minute, CronExpressionType.EveryWeekDay);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on specified days at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <param name="days">Days, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EverySpecificWeekDayAt(int hour, int minute, DaysOfWeek days)
        {
            var ce = new QuartzCronExpression(days, hour, minute, CronExpressionType.EverySpecificWeekDayAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every specific day of month 
        /// every *monthInterval* month at specified hours
        /// </summary>
        /// <param name="dayNumber">Day of the month, when occurence will happen</param>
        /// <param name="monthInterval">Interval in months</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EverySpecificDayEveryNMonthAt(int dayNumber, int monthInterval, int hour, int minute)
        {
            var ce = new QuartzCronExpression(dayNumber, monthInterval, hour, minute, CronExpressionType.EverySpecificDayEveryNMonthAt);
            return ce;
        }

        // TODO Add comments
        /// <summary>
        /// Create new CronExpression instance, which occurs on every first, 
        /// second, third or fourth day of the week each *monthInterval* months
        /// at specified hours
        /// </summary>
        /// <param name="dayNumber">Day sequental number (first, second, third, fourth)</param>
        /// <param name="days">Day of week</param>
        /// <param name="monthInverval">Interval in months</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber daySeqNumber, DaysOfWeek days, int monthInverval, int hour, int minute)
        {
            var ce = new QuartzCronExpression(daySeqNumber, days, monthInverval, hour, minute, CronExpressionType.EverySpecificSeqWeekDayEveryNMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on every specific day *dayNumber* of 
        /// specific month *month* at specified hours
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="dayNumber">Day number</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EverySpecificDayOfMonthAt(Months month, int dayNumber, int hour, int minute)
        {
            var ce = new QuartzCronExpression(month, dayNumber, hour, minute, CronExpressionType.EverySpecificDayOfMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on every first, 
        /// second, third or fourth day of the week on specific month *month*
        /// at specified hours
        /// </summary>
        /// <param name="dayNumber">Day sequental number (first, second, third, fourth)</param>
        /// <param name="days">Day of week</param>
        /// <param name="month">Month</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber dayNumber, DaysOfWeek days, Months month, int hour, int minute)
        {
            var ce = new QuartzCronExpression(dayNumber, days, month, hour, minute, CronExpressionType.EverySpecificSeqWeekDayOfMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on specific day of specified months
        /// at specific hour
        /// </summary>
        /// <param name="dayNumber">day number</param>
        /// <param name="months">month(s)</param>
        /// <param name="startHour">Hour, when occurence will happen</param>
        /// <param name="startMinute">Minute, when occurence will happen</param>
        /// <returns></returns>
        public static QuartzCronExpression EverySpecificDayOfSpecificMonthAt(int dayNumber, Months months, int startHour, int startMinute)
        {
            
            var ce = new QuartzCronExpression(
                dayNumber,
                months,
                startHour,
                startMinute,
                CronExpressionType.EverySpecificDayOfSpecificMonthAt);
            return ce;
        }


        /// <summary>
        /// Create new CronExpression instance, which occurs on specific day 
        /// at specific hour
        /// </summary>
        /// <param name="dateTime">Date and time</param>
        /// <returns>New CronExpression instance</returns>
        public static QuartzCronExpression SpecificDateAt(DateTime dateTime)
        {
            var ce = new QuartzCronExpression(
                dateTime.Second,
                dateTime.Minute,
                dateTime.Hour,
                dateTime.Day,
                dateTime.Month,
                dateTime.Year,
                CronExpressionType.SpecificDateAt);
            return ce;
        }

        public override string ToString()
        {
            return _cronString;
        }

        public static implicit operator string(QuartzCronExpression expression)
        {
            return expression.ToString();
        }
    }
}
