using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppExample
{
    public class QueueTrigger2
    {
        [FunctionName("QueueTrigger2")]
        public void Run([QueueTrigger("queueexample2", Connection = "MyAzureStorage")] Product product, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {product}");
        }
    }
}
