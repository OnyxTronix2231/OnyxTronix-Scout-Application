using System;
using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.CustomeEventModels
{
    public class CustomEventDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Key { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public List<CustomMatchDto> Matches { get; set; } = new();
    }
}
