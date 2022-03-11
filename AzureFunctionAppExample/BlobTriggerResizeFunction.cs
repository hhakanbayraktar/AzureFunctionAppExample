using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunctionAppExample
{
  public class BlobTriggerResizeFunction
  {
    [FunctionName("BlobTriggerResizeFunction")]
    public async Task Run([BlobTrigger("pictures/{name}", Connection = "MyAzureStorage")] Stream myBlob, string name, ILogger log,
      [Blob("pictures-resize/{name}", FileAccess.Write, Connection = "MyAzureStorage")] Stream outputBlobStream)
    {
      log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

      var fileType = await Image.DetectFormatAsync(myBlob);

      var resizeImage = Image.Load(myBlob);

      resizeImage.Mutate(x => x.Resize(100, 100));

      resizeImage.Save(outputBlobStream, fileType);

      log.LogInformation($"Picture resize process completed.");
    }
  }
}
