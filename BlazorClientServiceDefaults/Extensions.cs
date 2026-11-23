using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microsoft.Extensions.Hosting;

public static class BlazorClientExtensions
{
  public static WebAssemblyHostBuilder AddBlazorClientServiceDefaults(this WebAssemblyHostBuilder builder)
  {
    ComponentsMetricsServiceCollectionExtensions.AddComponentsMetrics(builder.Services);
    ComponentsMetricsServiceCollectionExtensions.AddComponentsTracing(builder.Services);

    var otlpPath = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
    if (string.IsNullOrWhiteSpace(otlpPath))
    {
      return builder;
    }

    var serviceName = builder.Configuration["OTEL_SERVICE_NAME"] ?? "BlazorWebAssembly";
    var otlpEndpoint = new Uri(new Uri(builder.HostEnvironment.BaseAddress), $"{otlpPath.TrimEnd('/')}/");

    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService(serviceName, serviceInstanceId: serviceName))
        .WithLogging(logging =>
        {
          logging.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint, "v1/logs"));
        }, options =>
        {
          options.IncludeFormattedMessage = true;
          options.IncludeScopes = true;
        })
        .WithMetrics(metrics =>
        {
          metrics.AddMeter("Microsoft.AspNetCore.Components");
          metrics.AddMeter("Microsoft.AspNetCore.Components.Lifecycle");
          metrics.AddHttpClientInstrumentation();
          metrics.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint, "v1/metrics"));
        })
        .WithTracing(tracing =>
        {
          tracing.AddSource("Microsoft.AspNetCore.Components")
              .AddHttpClientInstrumentation()
              .AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint, "v1/traces"));
        });

    return builder;
  }
}
