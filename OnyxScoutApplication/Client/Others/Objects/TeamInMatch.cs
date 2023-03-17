using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class TeamInMatch
    {
        public int TeamNumber { get; set; }

        public Match Match { get; set; }

        public bool IsFormExists { get; set; }

        public SimpleFormDto Form { get; set; }
    }
}
