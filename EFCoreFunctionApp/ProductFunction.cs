using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EFCoreFunctionApp.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreFunctionApp
{
  public class ProductFunction
  {
    private readonly AppDbContext _appDbContext;
    private const string Route = "Products";
    public ProductFunction(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    [FunctionName("GetProduct")]
    public async Task<IActionResult> GetProduct(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Route)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Get all products");

      var products = await _appDbContext.Products.ToListAsync();
      return new OkObjectResult(products);
    }

    [FunctionName("SaveProduct")]
    public async Task<IActionResult> SaveProduct(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = Route)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Save product");

      string body = await new StreamReader(req.Body).ReadToEndAsync();

      var newProduct = JsonConvert.DeserializeObject<Products>(body);

      _appDbContext.Products.Add(newProduct);

      await _appDbContext.SaveChangesAsync();

      return new OkObjectResult(newProduct);
    }

    [FunctionName("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = Route)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Save product");

      string body = await new StreamReader(req.Body).ReadToEndAsync();

      var newProduct = JsonConvert.DeserializeObject<Products>(body);

      _appDbContext.Products.Update(newProduct);

      await _appDbContext.SaveChangesAsync();

      return new NoContentResult();
    }

    [FunctionName("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = Route+"/{Id}")] HttpRequest req,
        ILogger log, int Id)
    {
      log.LogInformation("Save product");

      var product = await _appDbContext.Products.FindAsync(Id);

      _appDbContext.Products.Remove(product);

      await _appDbContext.SaveChangesAsync();

      return new NoContentResult();
    }
  }
}
