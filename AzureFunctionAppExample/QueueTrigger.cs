using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppExample
{
    public class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        public void Run([QueueTrigger("queueexample", Connection = "MyAzureStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
