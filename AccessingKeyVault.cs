using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Documents
{
    public class AccessingKeyVault
    {
        private readonly ILogger<AccessingKeyVault> _logger;

        public AccessingKeyVault(ILogger<AccessingKeyVault> logger)
        {
            _logger = logger;
        }

        [Function("AccessingKeyVault")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
