using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models
{
    public class CustomMatchDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public CustomEvent Event { get; set; }
        public int MatchNumber { get; set; }
        public string WinningAlliance { get; set; }
        public Alliances Alliances { get; set; }
        public string Level { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; }
    }

    public class AlliancesDto
    {
        public Alliance Blue { get; set; }
        public Alliance Red { get; set; }
    }

    public class AllianceDto
    {
        public int Score { get; set; }

        public List<string> TeamKeys { get; set; } = new List<string>();

        public int GetTeamAt(int index)
        {
            return int.Parse(TeamKeys[index].Replace("frc", ""));
        }
    }
}
