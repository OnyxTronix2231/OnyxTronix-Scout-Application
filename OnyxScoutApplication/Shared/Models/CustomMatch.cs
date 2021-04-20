using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models
{
    public class CustomMatch
    {
        public int Id { get; set; }
        [JsonIgnore]
        public CustomEvent Event { get; set; }
        public int MatchNumber { get; set; }
        public string WinningAlliance { get; set; }
        public CustomAlliances Alliances { get; set; }
        public string Level { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; }
    }

    public class CustomAlliances
    {
        public CustomAlliance Blue { get; set; }
        public CustomAlliance Red { get; set; }
    }

    public class CustomAlliance
    {
        public int Score { get; set; }

        public List<int> TeamKeys { get; set; } = new List<int>();

        public int GetTeamAt(int index)
        {
            return TeamKeys[index];
        }
    }
}
