﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData
{
    public class OptionSelectTeamFieldAverage : TeamFieldAverage
    {
        public Dictionary<string, OptionCalc> OptionsAverage { get; }
        public int TotalCount { get; set; }

        public OptionSelectTeamFieldAverage(FieldDto field) : base(field)
        {
            OptionsAverage = new Dictionary<string, OptionCalc>();
        }

        public override MarkupString GetFormattedAverage()
        {
            var v = new MarkupString(string.Join("<br />",
                OptionsAverage.Select(i => $"{i.Key}: {i.Value.Count}/{TotalCount}")));
            v = new MarkupString(v + "<br />" + GetRelativeValue().ToString("N2"));
            return v;
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if (other is OptionSelectTeamFieldAverage optionFieldAverage)
            {
                return GetRelativeValue().CompareTo(optionFieldAverage.GetRelativeValue());
            }

            throw new ArgumentException($"Cannot compare {nameof(NumericTeamFieldAverage)} type to {other.GetType()}");
        }

        public override double GetRelativeValue()
        {
            double value = 0;
            foreach (var option in OptionsAverage.Values)
            {
                value += option.Count * option.PercentWeight;
            }

            return value;
        }

       
    }
    
    public class OptionCalc
    {
        public int Count { get; init; }
        public float PercentWeight { get; init; }
    }
}
