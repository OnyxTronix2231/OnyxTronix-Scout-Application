using System;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Other.Analyzers.TeamData
{
    public class BooleanTeamFieldAverage : TeamFieldAverage
    {
        public int TrueCount { get; set; }

        public int TotalCount { get; set; }


        public BooleanTeamFieldAverage(FieldDto field) : base(field)
        {
        }

        public override MarkupString GetFormattedAverage()
        {
            return new((GetAverage() * 100).ToString("N2") + "%" + "<br />" + $"{TrueCount}/{TotalCount}");
        }

        public override double GetRelativeValue()
        {
            return GetAverage();
        }

        private float GetAverage()
        {
            return TrueCount / (float) TotalCount;
        }
    }
}
