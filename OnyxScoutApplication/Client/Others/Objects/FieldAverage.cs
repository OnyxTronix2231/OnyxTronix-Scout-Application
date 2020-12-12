using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class FieldAverage
    {
        public FieldDto Field { get; }
        public float Average { get; set; }
        public string Formated
        {
            get
            {
                return Field.FieldType switch
                {
                    FieldType.Boolean => (Average * 100).ToString("0.##") + "%",
                    FieldType.Numeric => Average.ToString("0.##"),
                    FieldType.CascadeField => (Average * 100).ToString("0.##") + "%",
                    _ => "",
                };
            }
        }
        public FieldAverage(FieldDto field, float average)
        {
            Field = field;
            Average = average;
        }
    }
}
