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
  public class HttpTrigger2
  {
    private readonly IService _service;
    public HttpTrigger2(IService service)
    {
      _service = service;
    }
    [FunctionName("HttpTrigger2")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation(_service.Write());
      log.LogInformation("Here");


      return new OkResult();
    }
  }
}
