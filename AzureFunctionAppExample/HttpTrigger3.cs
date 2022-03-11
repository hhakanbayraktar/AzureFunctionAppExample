using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Storage.Queue;

namespace AzureFunctionAppExample
{
  public static class HttpTrigger3
  {
    [FunctionName("HttpTrigger3")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log, [Table("Product", Connection = "MyAzureStorage")] CloudTable cloudTable, 
        [Queue("queueproduct", Connection = "MyAzureStorage")] CloudQueue cloudQueue)
    {
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      Product2 newProduct = JsonConvert.DeserializeObject<Product2>(requestBody);

      TableOperation tableOperation = TableOperation.Insert(newProduct);
      await cloudTable.ExecuteAsync(tableOperation);

      var productStr=JsonConvert.SerializeObject(newProduct);

      CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(productStr);
      await cloudQueue.AddMessageAsync(cloudQueueMessage);

      return new OkObjectResult(newProduct);
    }
  }
}
