﻿using System;
using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.CustomeEventModels
{
    public class CustomEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Key { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public List<CustomMatchDto> Matches { get; set; } = new List<CustomMatchDto>();
    }
}