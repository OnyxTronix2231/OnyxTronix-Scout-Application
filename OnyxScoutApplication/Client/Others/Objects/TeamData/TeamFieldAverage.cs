using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.TeamData
{
    public abstract class TeamFieldAverage : IComparable<TeamFieldAverage>
    {
        public FieldDto Field { get; }
       
        public TeamFieldAverage(FieldDto field)
        {
            Field = field;
        }

        public abstract MarkupString GetFormatedAverage();

        public abstract double GetRelativeValue();

        public abstract int CompareTo(TeamFieldAverage other);
    }
}
