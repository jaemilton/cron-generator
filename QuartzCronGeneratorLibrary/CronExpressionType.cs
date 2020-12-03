namespace QuartzCronGenerator
{
    public enum CronExpressionType
    {
        EveryNSeconds,
        EveryNMinutes,
        EveryNHours,
        EveryDayAt,
        EveryNDaysAt,
        EveryWeekDay,
        EverySpecificWeekDayAt,
        EverySpecificDayEveryNMonthAt,
        EverySpecificSeqWeekDayEveryNMonthAt,
        EverySpecificDayOfMonthAt,
        EverySpecificSeqWeekDayOfMonthAt,
        EverySpecificDayOfSpecificMonthAt,
        SpecificDateAt
    }
}
