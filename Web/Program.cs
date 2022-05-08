using Blazor.Analytics;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using SharedModels;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Web;
using Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

    services.AddScoped<AppState>();

    services.AddScoped<TableOfContents>();

    services.AddScoped<LazyAssemblyLoader>();

    services.AddHeadElementHelper();

    services.AddGoogleAnalytics("G-PY5ZL88NSM");
}
