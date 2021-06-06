using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnyxScoutApplication.Server.Data.Extensions
{
    public class FailResult : ContentResult
    {
        public FailResult(HttpStatusCode failCode, List<string> errors)
        {
            Errors = errors;
            StatusCode = (int?) failCode;
            Content = $"Status Code: {(int) failCode}; {failCode}; {string.Join(" ", errors)}";
            ContentType = "text/plain";
        }

        public IEnumerable<string> Errors { get; init; }
    }
}
