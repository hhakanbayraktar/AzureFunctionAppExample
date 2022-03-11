using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunctionAppExample
{
  public static class HttpTrigger4
  {
    [FunctionName("HttpTrigger4")]
    [return: Table("Product", Connection = "MyAzureStorage")]
    public static async Task<Product2> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
    ILogger log,
    [Queue("queueproduct", Connection = "MyAzureStorage")] CloudQueue cloudQueue)
    {
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      Product2 newProduct = JsonConvert.DeserializeObject<Product2>(requestBody);

      var productStr = JsonConvert.SerializeObject(newProduct);
      CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(productStr);
      await cloudQueue.AddMessageAsync(cloudQueueMessage);

      return newProduct;
    }
  }
}
