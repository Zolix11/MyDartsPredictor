namespace MyDartsPredictor.Dal.Entities
{
    public class DateWithTimeZone
    {
        public DateWithTimeZone(DateTime date, int timeZone)
        {
            this.Date = date;
            this.TimeZone = timeZone;
        }
        public DateTime Date { get; set; }
        public int TimeZone { get; set; }
    }
}
