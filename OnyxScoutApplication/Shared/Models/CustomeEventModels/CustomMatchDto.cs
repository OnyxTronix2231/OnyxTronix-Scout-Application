using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.CustomeEventModels
{
    public class CustomMatchDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int AlliancesId { get; set; }
        public CustomEventDto Event { get; set; } 
        public int MatchNumber { get; set; }
        public string WinningAlliance { get; set; }
        public CustomAlliancesDto Alliances { get; set; }
        public string Level { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }

    public class CustomAlliancesDto
    {
        public int Id { get; set; }
        public int BlueId { get; set; }
        public int RedId { get; set; }
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
        public int CustomAllianceId { get; set; }
        [JsonProperty("team_number")]
        public int TeamNumber { get; set; }
        public string Nickname { get; set; }
        [NotMapped]
        public string NameWithNumber => TeamNumber + " " + Nickname;
    }
}
