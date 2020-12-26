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

        public List<float> Values { get; } = new List<float>();
       
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
        public FieldAverage(FieldDto field)
        {
            Field = field;
        }

        public float[] GetSortedValues()
        {
            return Values.OrderByDescending(i => i).ToArray();
        }
    }
}
