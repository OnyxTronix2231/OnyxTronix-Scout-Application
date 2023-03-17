
using System;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Shared.Other.Analyzers;

public class AnalyticsSettings
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EventAnalyticSettingsDto EventAnalyticSettingsDto { get; set; }
}
