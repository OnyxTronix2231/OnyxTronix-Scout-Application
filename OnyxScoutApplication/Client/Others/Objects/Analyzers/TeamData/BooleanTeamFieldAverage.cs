using System;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData
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
            return new MarkupString(
                (GetAverage() * 100).ToString("0.##") + "%" + "<br />" + $"{TrueCount}/{TotalCount}");
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if (other is BooleanTeamFieldAverage booleanTeamFieldAverage)
            {
                return GetRelativeValue().CompareTo(booleanTeamFieldAverage.GetRelativeValue());
            }

            throw new ArgumentException($"Cannot compare {nameof(NumericTeamFieldAverage)} type to {other.GetType()}");
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
