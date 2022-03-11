using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppExample
{
  public class TimerTrigger
  {
    [FunctionName("TimerTrigger")]
    public void Run([TimerTrigger("* * * * * 3")] TimerInfo myTimer, ILogger log,
      [Blob("logs/{DateTime}.txt", System.IO.FileAccess.Write, Connection = "MyAzureStorage")] Stream blobStream)
    {
      var data = Encoding.UTF8.GetBytes($"Blob binding: {DateTime.Now}");

      blobStream.Write(data, 0, data.Length);
      blobStream.Close();
      blobStream.Dispose();

      log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    }
  }
}
