using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{

    public class Event
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Key { get; set; }
        public int Year { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }
    }
}
