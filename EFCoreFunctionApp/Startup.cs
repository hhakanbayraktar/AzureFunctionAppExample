using EFCoreFunctionApp.Model;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(EFCoreFunctionApp.Startup))]
namespace EFCoreFunctionApp
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      var conStr = Environment.GetEnvironmentVariable("SqlConStr");

      builder.Services.AddDbContext<AppDbContext>(options =>
      {
        options.UseSqlServer(conStr);
      });
    }
  }
}
