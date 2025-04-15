using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;


namespace Company.Function
{
    public class dotnet_testing_funapp
    {
        private readonly ILogger<dotnet_testing_funapp> _logger;

        public dotnet_testing_funapp(ILogger<dotnet_testing_funapp> logger)
        {
            _logger = logger;
        }

        [Function("dotnet_testing_funapp")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
