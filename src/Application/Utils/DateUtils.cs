namespace Engage.Application.Utils;

public static class DateUtils
{
    public static string SHORT_DATE_FORMAT = "dd MMM yyyy";

    public static string ShortDateString(DateTime startDate, DateTime? endDate)
    {
        return endDate.HasValue
            ? $"{startDate.ToString(SHORT_DATE_FORMAT)} - {endDate.Value.ToString(SHORT_DATE_FORMAT)}"
            : $"{startDate.ToString(SHORT_DATE_FORMAT)}";
    }

    public static string ShortDateString(DateTime startDate, DateTime endDate)
    {
        return $"{startDate.ToString(SHORT_DATE_FORMAT)} - {endDate.ToString(SHORT_DATE_FORMAT)}";
    }

    public static int MinutesBetweenDates(DateTime startDate, DateTime? endDate)
    {
        return endDate.HasValue
            ? (int)(endDate.Value - startDate).TotalMinutes
            : -1;
    }

}
