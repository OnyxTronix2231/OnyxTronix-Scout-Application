using System;
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

        public ScoutFormType Type { get; set; }

        public string KeyName { get; set; }
        
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
