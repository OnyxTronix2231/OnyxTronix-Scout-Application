using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnyxScoutApplication.Server.Data;
using OnyxScoutApplication.Server.Models;

[assembly: HostingStartup(typeof(OnyxScoutApplication.Server.Areas.Identity.IdentityHostingStartup))]

namespace OnyxScoutApplication.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
