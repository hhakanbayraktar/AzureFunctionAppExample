using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctionAppExample
{
  public class BlobTriggerResizeFunction2
  {
    [FunctionName("BlobTriggerResizeFunction2")]
    public async Task Run([BlobTrigger("pictures/{name}", Connection = "MyAzureStorage")] Stream myBlob, string name, ILogger log,
    [Blob("pictures-resize", Connection = "MyAzureStorage")] CloudBlobContainer cloudBlobContainer)
    {
      var fileType = await Image.DetectFormatAsync(myBlob);

      await cloudBlobContainer.CreateIfNotExistsAsync();

      var blockBlob = cloudBlobContainer.GetBlockBlobReference($"{Guid.NewGuid()}.{fileType.FileExtensions.First()}");


      log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");


      var resizeImage = Image.Load(myBlob);

      resizeImage.Mutate(x => x.Resize(100, 100));

      MemoryStream memoryStream = new MemoryStream();
      resizeImage.Save(memoryStream, fileType);
      memoryStream.Position = 0;

      blockBlob.UploadFromStream(memoryStream);
      memoryStream.Close();
      memoryStream.Dispose();

      log.LogInformation($"Picture resize process completed.");
    }
  }
}
