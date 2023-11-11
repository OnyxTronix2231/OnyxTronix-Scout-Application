using System;

namespace OnyxScoutApplication.Shared.Models;

public class ServerStatus
{
    public DateTime Date { get; set; }
    public bool IsAlive { get; set; }
}