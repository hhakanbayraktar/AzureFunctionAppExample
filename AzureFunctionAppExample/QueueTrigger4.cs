using System;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppExample
{
  public class QueueTrigger4
  {
    [FunctionName("QueueTrigger4")]
    public void Run([QueueTrigger("queueexample4", Connection = "MyAzureStorage")] string message, ILogger log,
      [Blob("pictures/{queueTrigger}", System.IO.FileAccess.Read, Connection = "MyAzureStorage")] CloudBlockBlob cloudBlockBlob)
    {
      log.LogInformation($"Blob Name: {cloudBlockBlob.Name}");
      log.LogInformation($"Blob Type: {cloudBlockBlob.BlobType}");
      log.LogInformation($"Content Type: {cloudBlockBlob.Properties.ContentType}");
    }
  }
}
