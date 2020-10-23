using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormDto
    {
        public int Id { get; set; }
        
        public int TeamNumber { get; set; }
     
        public int Year { get; set; }

        public string MatchName { get; set; }

        public string WriterUserName { get; set; }

        public List<ScoutFormDataDto> AutonomousData { get; set; } = new List<ScoutFormDataDto>();

        public List<ScoutFormDataDto> TeleoperatedData { get; set; } = new List<ScoutFormDataDto>();

        public List<ScoutFormDataDto> EndGameData { get; set; } = new List<ScoutFormDataDto>();
    }
}
