using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : Controller
{
    [HttpGet("ServerStatus")]
    public async Task<ActionResult<ServerStatus>> GetServerStatus()
    {
        return await Task.FromResult(new ServerStatus { Date = DateTime.Now, IsAlive = true });
    }
}
