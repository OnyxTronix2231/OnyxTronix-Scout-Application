using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData
{
    public class NumericTeamFieldAverage : TeamFieldAverage
    {
        public List<float> Values { get; } = new List<float>();
        public float Average { get; set; }

        public NumericTeamFieldAverage(FieldDto field) : base(field)
        {
        }

        public override MarkupString GetFormattedAverage()
        {
            return new MarkupString(Average.ToString("0.##"));
        }

        public string GetFormattedHighValuesByCount(int count)
        {
            return GetFormattedValues(GetSortedValues().Take(count).ToArray());
        }

        public string GetFormattedLowValuesByCount(int count)
        {
            return GetFormattedValues(GetSortedValues().TakeLast(count).ToArray());
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if (other is NumericTeamFieldAverage numricTeamFieldAverage)
            {
                return Average.CompareTo(numricTeamFieldAverage.Average);
            }

            throw new ArgumentException($"Cannot compare {nameof(NumericTeamFieldAverage)} type to {other.GetType()}");
        }

        public override double GetRelativeValue()
        {
            return Average;
        }
        
        private IEnumerable<float> GetSortedValues()
        {
            return Values.OrderByDescending(i => i).ToArray();
        }
        
        private static string GetFormattedValues(params float[] values)
        {
            return string.Join(" ", values);
        }
    }
}