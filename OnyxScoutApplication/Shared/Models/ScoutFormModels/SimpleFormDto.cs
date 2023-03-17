using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class SimpleFormDto : IComparable<SimpleFormDto>
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

        public ScoutFormType Type { get; set; }

        public string KeyName
        {
            get
            {
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
        }

        public string EventName { get; set; }
        
        public string MatchType { get; set; }
        
        public int? MatchNumber { get; set; }
        
        public int? SetNumber { get; set; }
        
        public string WriterUserName { get; set; }

        public bool IsImageUploaded { get; set; }
        
        public string ImageName { get; set; }
        
        public string ImageFileName { get; set; }
        
        public int CompareTo(SimpleFormDto other)
        {
            return other.DateTime.CompareTo(DateTime);
        }
    }
}
