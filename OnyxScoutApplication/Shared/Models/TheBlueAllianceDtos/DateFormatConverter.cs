using Newtonsoft.Json.Converters;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
