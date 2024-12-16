using Blazor.Analytics;
using CommonComponents.Models;
using CommonComponents.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
// using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using SharedComponents;
using SharedModels;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Web;
using Web.Services;

// QuestPDF.Settings.License = LicenseType.Community;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
    //.WriteTo.BrowserHttp($"{builder.Configuration.GetValue<string>("ApiBaseAddress")}ingest", controlLevelSwitch: new LoggingLevelSwitch(LogEventLevel.Error))
    .WriteTo.BrowserConsole(levelSwitch: new LoggingLevelSwitch(LogEventLevel.Warning))
    .CreateLogger();

builder.Logging
       //.ClearProviders() // Commented this as it affects default global blazor-error-ui from getting rendered.
       .AddProvider(new SerilogLoggerProvider());

if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment, builder.Configuration);

var host = builder.Build();
var lazyLoader = host.Services.GetRequiredService<LazyLoaderService>();
await lazyLoader.PreloadAsync();
await host.RunAsync();

static void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment webHostEnv, IConfiguration configuration)
{
    services.AddScoped<LazyLoaderService>();

    services.AddTransient<CustomHeaderMessageHandlerDemo>(sp => new(new HttpClientHandler()));

    services.AddScoped(sp =>
    {
        var httpClient = new HttpClient(sp.GetRequiredService<CustomHeaderMessageHandlerDemo>()) { BaseAddress = new Uri(webHostEnv.BaseAddress) };

        return httpClient;
    });

    services.AddHttpClient<GitHubService>(client => client.BaseAddress = new Uri("https://api.github.com/"));

    services.AddScoped<AppState>();

    services.AddScoped<AppStateDemo>();

    services.AddScoped<TableOfContents>();

    services.AddScoped<Achievements>();

    services.AddScoped<Sitemaps>();

    services.AddTransient<AdSpaceService>();

    services.AddScoped<LazyAssemblyLoader>();

    services.AddHeadElementHelper();

    services.AddGoogleAnalytics("G-PY5ZL88NSM");

    services.AddGoogleAdSense("ca-pub-1536083653226834");

    services.AddTransient<TransientServiceDemo>();

    services.AddScoped<ScopedServiceDemo>();

    services.AddSingleton<SingletonServiceDemo>();

    services.AddHotKeys2();

    services.AddSingleton<IHostEnvironment, WebHostEnvironment>();

    services.AddScoped(serviceProvider => new SlugService(serviceProvider.GetRequiredService<NavigationManager>()));

    if (!webHostEnv.Environment.Equals("Prerendering", StringComparison.OrdinalIgnoreCase))
    {
        services.AddOidcAuthentication(options =>
        {
            options.ProviderOptions.Authority = "https://demo.duendesoftware.com";
            options.ProviderOptions.ClientId = "interactive.public.short";
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.DefaultScopes.Add("openid");
            options.ProviderOptions.DefaultScopes.Add("profile");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("offline_access");
            options.ProviderOptions.DefaultScopes.Add("api");
            options.ProviderOptions.RedirectUri = $"{webHostEnv.BaseAddress}authentication/login-callback";
            options.ProviderOptions.PostLogoutRedirectUri = $"{webHostEnv.BaseAddress}authentication/logout-callback";

            options.UserOptions.RoleClaim = "role";
            options.UserOptions.NameClaim = "name";
        });

        services.AddAuthorizationCore();

        services.AddCascadingAuthenticationState();
    }
}