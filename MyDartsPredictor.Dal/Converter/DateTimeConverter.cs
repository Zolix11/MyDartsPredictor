using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDartsPredictor.Dal.Entities;

public class DateWithTimeZoneConverter : ValueConverter<DateWithTimeZone, string>
{
    public DateWithTimeZoneConverter() : base(
        v => ConvertToString(v),
        v => ConvertToDateWithTimeZone(v))
    {
    }

    private static string ConvertToString(DateWithTimeZone v)
    {
        return $"{v.Date:yyyy-MM-ddTHH:mm:ss} {v.TimeZone}";
    }

    private static DateWithTimeZone ConvertToDateWithTimeZone(string v)
    {
        var parts = v.Split(' ');
        var date = DateTime.Parse(parts[0]);
        var timeZone = int.Parse(parts[1]);
        return new DateWithTimeZone(date, timeZone);
    }
}