using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnyxScoutApplication.Shared.Models
{
    public class CustomMatchDto
    {
        public int Id { get; set; }
        [Newtonsoft.Json.JsonIgnore]
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
        public int Id { get; set; }
        public int Score { get; set; }

        public List<CustomTeamDto> Teams { get; set; } = new List<CustomTeamDto>();

        public int GetTeamAt(int index)
        {
            return Teams[index].TeamNumber;
        }
    }
    
    public class CustomTeamDto
    {
        public int Id { get; set; }
        [JsonPropertyName("team_number")]
        public int TeamNumber { get; set; }
        public string Nickname { get; set; }
        [NotMapped]
        public string NameWithNumber => Nickname  + " " + TeamNumber;
    }
}
