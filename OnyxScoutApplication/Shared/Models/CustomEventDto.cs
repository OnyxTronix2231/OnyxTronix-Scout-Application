using System;
using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models
{
    public class CustomEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Key { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public List<CustomMatch> Matches { get; set; }
    }
}
