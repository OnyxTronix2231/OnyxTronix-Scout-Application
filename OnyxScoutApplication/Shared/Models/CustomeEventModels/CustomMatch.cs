using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.CustomeEventModels
{
    [FirestoreData]
    public class CustomMatch
    {
       // public int Id { get; set; }
        //public int EventId { get; set; }
       // public int AlliancesId { get; set; }
       // [JsonIgnore]
       // public CustomEvent Event { get; set; }
       [FirestoreProperty]
        public int MatchNumber { get; set; }
        [FirestoreProperty]
        public string WinningAlliance { get; set; }
        [JsonIgnore]
        [FirestoreProperty]
        public CustomAlliances Alliances { get; set; }
        [FirestoreProperty]
        public string Level { get; set; }
        [FirestoreProperty]
        public string Key { get; set; }
        [FirestoreProperty]
        public DateTime Date { get; set; }
    }

    [FirestoreData]
    public class CustomAlliances
    {
        //   public int Id { get; set; }
        // [ForeignKey("Blue")]
        // public int? BlueId { get; set; }
        // [ForeignKey("Red")]
        // public int? RedId { get; set; }
        [FirestoreProperty]
        public CustomAlliance Blue { get; set; }

        [FirestoreProperty]
        public CustomAlliance Red { get; set; }
    }

    [FirestoreData]
    public class CustomAlliance
    {
      //  public int Id { get; set; }
      [FirestoreProperty]
        public int Score { get; set; }

        [FirestoreProperty]
        public List<CustomTeam> Teams { get; set; } = new();

        public int GetTeamAt(int index)
        {
            return Teams[index].TeamNumber;
        }
    }
    
    [FirestoreData]
    public class CustomTeam
    {
        [FirestoreProperty]
        public int TeamNumber { get; set; }
        [FirestoreProperty]
        public string Nickname { get; set; }
       
    }
}
