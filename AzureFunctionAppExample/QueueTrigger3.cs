using System;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppExample
{
    public class QueueTrigger3
    {
        [FunctionName("QueueTrigger3")]
        public void Run([QueueTrigger("queueexample3", Connection = "MyAzureStorage")]CloudQueueMessage cloudQueueMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {cloudQueueMessage.AsString}");
        }
    }
}
