namespace Byndyusoft.Template.IntegrationTests.Fixtures
{
    using System;
    using System.IO;
    using Api.Installers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class TestServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public TestServiceProvider()
        {
            var configuration = CreateConfiguration();

            _serviceProvider = CreateServiceProvider(configuration);
        }

        private IConfiguration CreateConfiguration()
        {
            var path = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder().SetBasePath(path)
                                                          .AddJsonFile("appsettings.Development.json", false, false)
                                                          .AddEnvironmentVariables()
                                                          .Build();

            return configuration;
        }

        private IServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            var serviceProvider = new ServiceCollection().AddApplicationServices()
                                                         .AddLogging()
                                                         .BuildServiceProvider();

            return serviceProvider;
        }

        public TService GetService<TService>()
            where TService : class
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}