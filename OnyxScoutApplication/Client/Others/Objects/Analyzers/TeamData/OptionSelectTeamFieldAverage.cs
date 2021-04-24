using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData
{
    public class OptionSelectTeamFieldAverage : TeamFieldAverage
    {
        public Dictionary<string, Tuple<float, float>> OptionsAverage { get; }

        public OptionSelectTeamFieldAverage(FieldDto field) : base(field)
        {
            OptionsAverage = new Dictionary<string, Tuple<float, float>>();
        }

        public override MarkupString GetFormattedAverage()
        {
            return new MarkupString(string.Join("<br />",
                OptionsAverage.Select(i => $"{i.Key}: {i.Value.Item1}/{i.Value.Item2}")));
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
            return 0; //TODO: calculate relative value
        }
    }
}
