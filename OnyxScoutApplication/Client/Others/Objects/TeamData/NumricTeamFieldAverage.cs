using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.TeamData
{
    public class NumricTeamFieldAverage : TeamFieldAverage
    {

        public List<float> Values { get; } = new List<float>();
        public float Average { get; set; }

        public NumricTeamFieldAverage(FieldDto field) : base(field)
        {
        }

        public override MarkupString GetFormatedAverage()
        {
            return new MarkupString(Average.ToString("0.##"));
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


        private string GetFormatedValues(params float[] values)
        {
            string text = "";
            foreach (var item in values)
            {
                text += item + " ";

            }
            return text;
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if(other is NumricTeamFieldAverage numricTeamFieldAverage)
            {
                return Average.CompareTo(numricTeamFieldAverage.Average);
            }
            throw new ArgumentException($"Cannot compare {nameof(NumricTeamFieldAverage)} type to {other.GetType()}");
        }
    }
}
