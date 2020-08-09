﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormDto
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int TeamNumber { get; set; }
     
        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(30)]
        public string MatchName { get; set; }

        public List<ScoutFormDataDto> AutonomousData { get; set; } = new List<ScoutFormDataDto>();

        public List<ScoutFormDataDto> TeleoperatedData { get; set; } = new List<ScoutFormDataDto>();

        public List<ScoutFormDataDto> EndGameData { get; set; } = new List<ScoutFormDataDto>();
    }
}