using Microsoft.AspNetCore.Components;
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

        public Dictionary<string, float> optionsAvarage = new Dictionary<string, float>();
       
        public FieldAverage(FieldDto field)
        {
            Field = field;
        }

        public MarkupString GetFormatedAverage()
        {
            return Field.FieldType switch
            {
                FieldType.Boolean => new MarkupString((Average * 100).ToString("0.##") + "%"),
                FieldType.Numeric => new MarkupString(Average.ToString("0.##")),
                FieldType.CascadeField => new MarkupString((Average * 100).ToString("0.##") + "%"),
                FieldType.OptionSelect => new MarkupString(GetFormatedOptionsAvarage()),
                _ => new MarkupString(),
            };
        }

        public float[] GetSortedValues()
        {
            return Values.OrderByDescending(i => i).ToArray();
        }

        public string GetFormatedHighValuesByCount(int count)
        {
            return GetFormatedValues(GetSortedValues().Take(count).ToArray());
        }

        public string GetFormatedLowValuesByCount(int count)
        {
            return GetFormatedValues(GetSortedValues().TakeLast(count).ToArray());
        }

        public string GetFormatedOptionsAvarage()
        {
            return string.Join("<br />", 
                optionsAvarage.Select(i => $"{i.Key}: {(i.Value * 100).ToString("0.##") + "%"}"));
        }

        private string GetFormatedValues(params float[] values)
        {
            string text = "";
            foreach (var item in values)
            {
                text += item + " ";

            }
            return text;
        }
    }
}
