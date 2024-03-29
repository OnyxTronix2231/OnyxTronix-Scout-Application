using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDto : IComparable<FormDto>
    {
        private DateTimeOffset dateTime;
        public string Id { get; set; }

        public DateTimeOffset DateTime
        {
            get => dateTime;
            set => dateTime = value.ToLocalTime();
        }

        public int TeamNumber { get; set; }

        public int Year { get; set; }

        public bool ImageRequired { get; set; } = false;

        public ScoutFormType Type { get; set; }

        public string KeyName
        {
            get
            {
                // if (MatchType == "" || MatchNumber == null)
                // {
                //     return EventName;
                // }
                if (MatchType == null)
                {
                    return $"{EventName}_pitForm";
                }
                
                if (MatchType == "qm")
                {
                    return $"{EventName}_{MatchType}{MatchNumber}";
                }
                return $"{EventName}_{MatchType}{SetNumber}m{MatchNumber}";
            }
            // set
            // {
            //     if (value == null)
            //     {
            //         return;
            //     }
            //     EventName = value.Split("_")[0];
            //     var matchDetails = value.Contains("_") ? value.Split("_")[1] : "";
            //     if (string.IsNullOrWhiteSpace(matchDetails))
            //     {
            //         //MatchType = "qm";
            //         MatchType = "";
            //         MatchNumber = null;
            //         return;
            //     }
            //     MatchType = string.Join("", new Regex("[a-z]{0,2}.?[a-z]").Matches(matchDetails));
            //     if (int.TryParse(string.Join("", new Regex("[0-9]{0,2}$").Matches(matchDetails)), out int res))
            //     {
            //         MatchNumber = res;
            //     }
            // }
        }

        public string EventName { get; set; }
        
        public string MatchType { get; set; }
        
        public int? MatchNumber { get; set; }
        
        public int? SetNumber { get; set; }
        
        public string WriterUserName { get; set; }

        public List<FormDataInStageDto> FormDataInStages { get; set; } = new();
        
        public bool IsImageUploaded { get; set; }
        
        public string ImageName { get; set; }
        
        public string ImageFileName { get; set; }
        
        public int CompareTo(FormDto other)
        {
            return other.DateTime.CompareTo(DateTime);
        }
    }
}
