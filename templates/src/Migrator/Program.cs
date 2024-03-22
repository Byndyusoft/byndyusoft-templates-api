namespace Byndyusoft.Template.Migrator;

using System;
using FluentMigrator.Runner;
using Logging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

internal class Program
{
    private static int Main()
    {
        var serviceProvider = CreateServices();

        using var scope = serviceProvider.CreateScope();

        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        try
        {
            runner.MigrateUp();
        }
        catch (Exception)
        {
            return -1;
        }

        return 0;
    }

    private static IServiceProvider CreateServices()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{environmentName}.json", true)
                            .AddEnvironmentVariables()
                            .Build();

        Log.Logger = new LoggerConfiguration().UseDefaultEnrichSettings("Migrator")
                                              .OverrideDefaultLoggers()
                                              .UseConsoleWriterSettings()
                                              .CreateLogger();

        return new ServiceCollection()
               .AddFluentMigratorCore()
               .ConfigureRunner(rb => rb
                                      .AddPostgres()
                                      .WithGlobalConnectionString(configuration.GetConnectionString("Main"))
                                      .ScanIn(typeof(Program).Assembly).For.Migrations())
               .AddLogging(lb => lb.ClearProviders().AddSerilog(Log.Logger, dispose: true))
               .BuildServiceProvider(false);
    }
}