using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.TeamData
{
    public class OptionSelectTeamFieldAverage : TeamFieldAverage
    {

        public Dictionary<string, Tuple<float, float>> OptionsAvarage { get; set; }

        public OptionSelectTeamFieldAverage(FieldDto field) : base(field)
        {
            OptionsAvarage = new Dictionary<string, Tuple<float, float>>();
        }

        public override MarkupString GetFormatedAverage()
        {
            return new MarkupString(string.Join("<br />",
                OptionsAvarage.Select(i => $"{i.Key}: {i.Value.Item1}/{i.Value.Item2}")));
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if (other is OptionSelectTeamFieldAverage booleanTeamFieldAverage)
            {
                throw new NotImplementedException("Option select sorting not implemented yet!");
            }
            throw new ArgumentException($"Cannot compare {nameof(NumricTeamFieldAverage)} type to {other.GetType()}");
        }

        public override double GetRelativeValue()
        {
            return 0;
        }
    }
}
