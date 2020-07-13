using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormFormat
    {
        public int Id { get; set; }
        
        public int Year { get; set; }

        public List<Field> Fields { get; set; } = new List<Field>();
    }

}
