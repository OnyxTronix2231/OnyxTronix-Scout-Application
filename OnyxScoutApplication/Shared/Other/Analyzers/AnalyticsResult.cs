using System.Collections.Generic;
using System.Dynamic;

namespace OnyxScoutApplication.Shared.Other.Analyzers;

public class AnalyticsResult
{
    public List<ExpandoObject> CalculatedTeamsData { get; set; }

    public List<ColumnField> ColumnsFields { get; set; }
}
