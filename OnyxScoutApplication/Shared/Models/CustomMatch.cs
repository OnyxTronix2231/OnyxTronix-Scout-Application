using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Shared.Models
{
    public class CustomMatch
    {
        public int Id { get; set; }
        [Newtonsoft.Json.JsonIgnore]
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
        public int Id { get; set; }
        public CustomAlliance Blue { get; set; } 
        public CustomAlliance Red { get; set; }
    }

    public class CustomAlliance
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public List<CustomTeam> Teams { get; set; } = new List<CustomTeam>();

        public int GetTeamAt(int index)
        {
            return Teams[index].TeamNumber;
        }
    }
    
    public class CustomTeam
    {
        public int Id { get; set; }
        public int TeamNumber { get; set; }
        public string Nickname { get; set; }
       
    }
}
