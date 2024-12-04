using Byndyusoft.Logging.Configuration;
using Byndyusoft.MaskedSerialization.Serilog.Extensions;
using Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;
using Byndyusoft.Template.Api.Infrastructure.Swagger;
using Byndyusoft.Template.Api.Infrastructure.Versioning;
using Byndyusoft.Template.Api.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;
using System.Text.Json.Serialization;

var serviceName = typeof(Program).Assembly.GetName().Name;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
                            configuration
                                .UseDefaultSettings(context.Configuration)
                                .UseOpenTelemetryTraces()
                                .WriteToOpenTelemetry()
                                .WithMaskingPolicy());
// Add services to the container.
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddOpenTelemetry(serviceName,
                          builder.Configuration.GetSection("OtlpExporterOptions").Bind,
                          builder => builder.AddNpgsql(),
                          builder => builder.AddTemplateMetrics());
services
    .AddMvcCore()
    .AddTracing();

services
    .AddRouting(options => options.LowercaseUrls = true)
    .AddControllers()
    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

services.AddHealthChecks();
services
    .AddVersioning()
    .AddSwagger();
services
    .AddRelationalDb(NpgsqlFactory.Instance, builder.Configuration.GetConnectionString("Main"))
    .AddApplicationServices();

var app = builder.Build();
app
    .UseHealthChecks("/healthz")
    .UseOpenTelemetryPrometheusScrapingEndpoint()
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.Run();

// For tests accessibility
public partial class Program { }