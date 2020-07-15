using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormFormatDto
    {
        public int Id { get; set; }
        
        public int Year { get; set; }

        public List<FieldDto> AutonomousFields { get; set; } = new List<FieldDto>();

        public List<FieldDto> TeleoperatedFields { get; set; } = new List<FieldDto>();

        public List<FieldDto> EndGameFields { get; set; } = new List<FieldDto>();      
    }

}
