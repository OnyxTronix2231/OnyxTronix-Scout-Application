using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{
    public class Team
    {
        [JsonProperty("team_number")]
        public int TeamNumber { get; set; }

        public string Nickname { get; set; }
    }
}
