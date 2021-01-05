﻿using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.TeamData
{
    public class BooleanTeamFieldAverage : TeamFieldAverage
    {

        public int TrueCount { get; set; }

        public int TotalCount { get; set; }


        public BooleanTeamFieldAverage(FieldDto field) : base(field)
        {
        }

        public float GetAverage()
        {
            return TrueCount / (float) TotalCount;
        }

        public override MarkupString GetFormatedAverage()
        {
            return new MarkupString((GetAverage() * 100).ToString("0.##") + "%" + "<br />" + $"{TrueCount}/{TotalCount}");
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            if (other is BooleanTeamFieldAverage booleanTeamFieldAverage)
            {
                return GetAverage().CompareTo(booleanTeamFieldAverage.GetAverage());
            }
            throw new ArgumentException($"Cannot compare {nameof(NumricTeamFieldAverage)} type to {other.GetType()}");
        }
    }
}
