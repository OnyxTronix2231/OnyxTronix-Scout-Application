using System;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

public abstract class TeamFieldAverage : IComparable<TeamFieldAverage>
{
    public FieldDto Field { get; }

    protected TeamFieldAverage(FieldDto field)
    {
        Field = field;
    }

    public abstract MarkupString GetFormattedAverage();

    public abstract double GetRelativeValue();

    public abstract int CompareTo(TeamFieldAverage other);
}
