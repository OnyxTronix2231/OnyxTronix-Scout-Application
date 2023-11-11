using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Other.Analyzers.TeamData
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
            return new($"<b>{Average:N2}</b><br>{string.Join(",", Values)}");
        }

        public string GetFormattedHighValuesByCount(int count)
        {
            return GetFormattedValues(GetSortedValues().Take(count).ToArray());
        }

        public string GetFormattedLowValuesByCount(int count)
        {
            return GetFormattedValues(GetSortedValues().TakeLast(count).ToArray());
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
