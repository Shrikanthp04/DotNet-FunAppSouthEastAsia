using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Function
{
    public class AccessingDBConnectionStr
    {
        private readonly ILogger<AccessingDBConnectionStr> _logger;

        public AccessingDBConnectionStr(ILogger<AccessingDBConnectionStr> logger)
        {
            _logger = logger;
        }

        [Function("AccessingDBConnectionStr")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Fetching DB connection string from Key Vault...");

            string secretName = "DBConnectionStr";
            var kvUri = $"https://dotnetdbconnectionstring.vault.azure.net/";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            try
            {
                KeyVaultSecret secret = await client.GetSecretAsync(secretName);
                string dbConnectionStr = secret.Value;

                var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
                await response.WriteStringAsync($"DB Connection String: {dbConnectionStr}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching secret: {ex.Message}");

                var response = req.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
                await response.WriteStringAsync("Failed to fetch secret from Key Vault.");
                return response;
            }
        }
    }
}
