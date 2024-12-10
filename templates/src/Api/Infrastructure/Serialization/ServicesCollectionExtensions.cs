namespace Byndyusoft.Template.Api.Infrastructure.Serialization
{
    using System.Text.Json.Serialization;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddJsonSerializerOptions(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllers()
                .AddJsonOptions(
                    options =>
                        options
                            .JsonSerializerOptions
                            .Converters
                            .Add(new JsonStringEnumConverter())
                );
            return serviceCollection;
        }
    }
}