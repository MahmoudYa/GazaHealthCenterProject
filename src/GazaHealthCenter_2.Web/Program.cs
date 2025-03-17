using System.Diagnostics.CodeAnalysis;

namespace GazaHealthCenter_2.Web;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(String[] args)
    {
        new WebHostBuilder()
            .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
            .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
            .UseKestrel(options => options.AddServerHeader = false)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseIISIntegration()
            .UseIIS()
            .Build()
            .Run();
    }
}
