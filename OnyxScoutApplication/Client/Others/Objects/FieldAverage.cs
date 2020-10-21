using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class FieldAverage
    {
        public FieldDto Field { get; set; }
        public float Average { get; set; }
    }
}
