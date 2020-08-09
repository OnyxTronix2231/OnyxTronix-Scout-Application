using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{
    public class Team
    {
        [JsonPropertyName("team_number")]
        public int TeamNumber { get; set; }

        public string Nickname { get; set; }
    }
}
