namespace WeatherWebAPI.Models.FiveDaysModel.DayModel
{
    public class TimeLines
    {
        public ICollection<DayUnit>? Daily { get; set; } = new List<DayUnit>();
    }
}
