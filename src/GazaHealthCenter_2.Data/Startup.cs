using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace GazaHealthCenter_2.Data;

[ExcludeFromCodeCoverage]
public class Startup
{
    private String Connection { get; }

    public Startup(IHostEnvironment host)
    {
        Connection = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(host.ContentRootPath)!.FullName)
            .AddJsonFile("GazaHealthCenter_2.Web/configuration.json")
            .AddJsonFile($"GazaHealthCenter_2.Web/configuration.{host.EnvironmentName.ToLower()}.json", optional: true)
            .Build()["Data:Connection"];
    }

    public void Configure()
    {
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<Context>(options => options.UseSqlServer(Connection));
    }
}
