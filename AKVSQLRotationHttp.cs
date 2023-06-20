using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace KeyVaultRotator
{
    public static class AKVSQLRotationHttp
    {
        [FunctionName("AKVSQLRotationHttp")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string keyVaultName = req.Query["KeyVaultName"];
            string secretName = req.Query["SecretName"];
            if (string.IsNullOrEmpty(keyVaultName) || string.IsNullOrEmpty(secretName))
            {
                return new BadRequestObjectResult("Please pass a KeyVaultName and SecretName on the query string");
            }

            log.LogInformation(req.ToString());

            log.LogInformation("C# Http trigger function processed a request.");
            SecretRotator.RotateSecret(log, secretName, keyVaultName);

            return new OkObjectResult($"Secret Rotated Successfully");
        }
    }
}
