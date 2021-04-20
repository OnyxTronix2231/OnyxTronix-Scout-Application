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
        public CustomAlliancesDto Alliances { get; set; }
        public string Level { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; }
    }

    public class CustomAlliancesDto
    {
        public CustomAllianceDto Blue { get; set; }
        public CustomAllianceDto Red { get; set; }
    }

    public class CustomAllianceDto
    {
        public int Score { get; set; }

        public List<string> TeamKeys { get; set; } = new List<string>();

        public int GetTeamAt(int index)
        {
            return int.Parse(TeamKeys[index].Replace("frc", ""));
        }
    }
}
