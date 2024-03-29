﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{

    public class Match
    {
        [JsonProperty("match_number")]
        public int MatchNumber { get; set; }
        
        [JsonProperty("set_number")]
        public int SetNumber { get; set; }
        
        [JsonProperty("event_key")]
        public string EventKey { get; set; }

        public string FullMatchNumber
        {
            get => $"{MatchNumber}({SetNumber})";
        }

        [JsonProperty("winning_alliance")]
        public string WinningAlliance { get; set; }

        public Alliances Alliances { get; set; }

        [JsonProperty("comp_level")]
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
        
        public bool IsInTeamBlue(int teamNumber)
        {
            return Blue.TeamKeys.Any(i => i.Replace("frc", "").Equals(teamNumber.ToString()));
        }
        
        public bool IsInTeamRed(int teamNumber)
        {
            return Red.TeamKeys.Any(i => i.Replace("frc", "").Equals(teamNumber.ToString()));
        }
    }

    public class Alliance
    {
        public int Score { get; set; }
        [JsonProperty("team_keys")]
        public List<string> TeamKeys { get; set; } = new List<string>();

        public int GetTeamAt(int index)
        {
            return int.Parse(TeamKeys[index].Replace("frc", ""));
        }
    }
}
