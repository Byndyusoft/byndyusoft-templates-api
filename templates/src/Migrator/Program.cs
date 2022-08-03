namespace Byndyusoft.Template.Migrator
{
    using System;
    using FluentMigrator.Runner;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class Program
    {
        private static void Main()
        {
            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();

            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        private static IServiceProvider CreateServices()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environmentName}.json", true)
                                .AddEnvironmentVariables()
                                .Build();


            return new ServiceCollection()
                   .AddFluentMigratorCore()
                   .ConfigureRunner(rb => rb
                                          .AddPostgres()
                                          .WithGlobalConnectionString(configuration.GetConnectionString("Main"))
                                          .ScanIn(typeof(Program).Assembly).For.Migrations())
                   .AddLogging(lb => lb.AddFluentMigratorConsole())
                   .BuildServiceProvider(false);
        }
    }
}