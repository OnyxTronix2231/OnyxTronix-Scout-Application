using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{

    public class Match
    {
        [JsonPropertyName("match_number")]
        public int MatchNumber { get; set; }

        [JsonPropertyName("winning_alliance")]
        public string WinningAlliance { get; set; }

        public Alliances Alliances { get; set; }

        [JsonPropertyName("comp_level")]
        public string Level { get; set; }

        public string Key { get; set; }

        public DateTime Date { get; set; }

        public int Time
        {
            set
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                Date = dtDateTime.AddSeconds(value).ToLocalTime();
            }
        }
    }

    public class Alliances
    {
        public Alliance Blue { get; set; }
        public Alliance Red { get; set; }
    }

    public class Alliance
    {
        public int Score { get; set; }
        public List<int> TeamKeys { get; set; } = new List<int>();

        public int GetTeamAt(int index)
        {
            return TeamKeys[index];
        }
    }
}
