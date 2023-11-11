
using System;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Shared.Other.Analyzers;

public class AnalyticsSettings
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public EventAnalyticSettingsDto EventAnalyticSettingsDto { get; set; }
}
