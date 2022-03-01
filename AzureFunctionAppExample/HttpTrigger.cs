using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionAppExample
{
  public static class HttpTrigger
  {
    [FunctionName("HttpTriggerQueryStr")]
    public static IActionResult GetProductsQueryStr(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "products_query")] HttpRequest req,
        ILogger log)
    {
      string id = req.Query["id"];

      log.LogInformation($"Product ID:{id}");

      return new OkResult();
    }

    [FunctionName("HttpTriggerRoute")]
    public static IActionResult GetProductRoute(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "products/{id}")] HttpRequest req,
        ILogger log, int id)
    {
      log.LogInformation($"Product ID:{id}");

      return new OkResult();
    }

    //{
    //"id" : 23,
    //"name" : "Printer",
    //"Price" : 100.25,
    //"Category" : "Test" 
    //}

    [FunctionName("HttpTriggerBody")]
    public static async Task<IActionResult> GetProductBody(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products_body")] HttpRequest req,
          ILogger log)
    {
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

      Product product = JsonConvert.DeserializeObject<Product>(requestBody);

      log.LogInformation($"Product ID:{product.Id}");

      return new OkObjectResult(product);
    }

    [FunctionName("HttpTriggerLocalSettingJson")]
    public static IActionResult LocalSettingJson(
   [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "localsettingjson")] HttpRequest req,
   ILogger log)
    {
      string myAPI = Environment.GetEnvironmentVariable("MyAPI");
      log.LogInformation($"API Key: {myAPI}");

      return new OkResult();
    }
  }
}
