using Blazor.Analytics;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using SharedComponents;
using SharedModels;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Web;
using Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

    services.AddScoped<AppState>();

    services.AddScoped<AppStateDemo>();

    services.AddScoped<TableOfContents>();

    services.AddScoped<Sitemaps>();

    services.AddTransient<AdSpaceService>();

    services.AddScoped<LazyAssemblyLoader>();

    services.AddHeadElementHelper();

    services.AddGoogleAnalytics("G-PY5ZL88NSM");

    services.AddGoogleAdSense("ca-pub-1536083653226834");

    services.AddTransient<TransientServiceDemo>();

    services.AddScoped<ScopedServiceDemo>();

    services.AddSingleton<SingletonServiceDemo>();
}
