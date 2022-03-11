using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AzureFunctionAppExample
{
  public class BlobTrigger
  {
    //"pictures/{name}.png" Listen only PNG
    [FunctionName("BlobTrigger")]
    public void Run([BlobTrigger("pictures/{name}", Connection = "MyAzureStorage")] Stream myBlob, string name, ILogger log)
    {
      log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }
  }
}
