using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace OnyxScoutApplication.Server.Controllers
{
    public class OidcConfigurationController : Controller
    {
        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider)
        {
            this.clientRequestParametersProvider = clientRequestParametersProvider;
        }

        private readonly IClientRequestParametersProvider clientRequestParametersProvider;

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = clientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
