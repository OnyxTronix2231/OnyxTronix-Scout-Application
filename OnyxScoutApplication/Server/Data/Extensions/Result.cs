using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Extensions
{
    public class Result
    {
        public static ActionResult ResultCode(HttpStatusCode statusCode, string reason) => new ContentResult
        {
            StatusCode = (int) statusCode,
            Content = $"Status Code: {(int) statusCode}; {statusCode}; {reason}",
            ContentType = "text/plain",
        };
    }
}
