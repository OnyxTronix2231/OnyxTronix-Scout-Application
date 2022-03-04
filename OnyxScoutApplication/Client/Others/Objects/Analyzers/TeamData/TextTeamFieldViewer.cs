﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData
{
    public class TextTeamFieldViewer : TeamFieldAverage
    {
        public List<string> Texts { get; }


        public TextTeamFieldViewer(FieldDto field) : base(field)
        {
            Texts = new List<string>();
        }

        public override MarkupString GetFormattedAverage()
        {
            return (MarkupString) string.Join("<br />", Texts.ToArray());
        }

        public override int CompareTo(TeamFieldAverage other)
        {
            return 0;
        }

        public override double GetRelativeValue()
        {
            return GetAverage();
        }

        private float GetAverage()
        {
            return 0;
        }
    }
}
