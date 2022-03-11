using Microsoft.Azure.Cosmos.Table;

namespace AzureFunctionAppExample
{
  public class Product2 : TableEntity
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Color { get; set; }
  }
}
