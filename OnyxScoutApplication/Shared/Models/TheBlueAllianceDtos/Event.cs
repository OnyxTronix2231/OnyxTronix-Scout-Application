using System;
using System.Collections.Generic;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos
{

    public class Event
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Key { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
    }
}
