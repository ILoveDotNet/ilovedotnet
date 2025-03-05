using Blazor.Analytics;
using CommonComponents.Models;
using CommonComponents.Services;
using Duende.IdentityModel.OidcClient;
using MAUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.Extensions.Logging;
using SharedComponents;
using SharedModels;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace MAUI;
public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });

    builder.Services.AddMauiBlazorWebView();

#if DEBUG
    builder.Services.AddBlazorWebViewDeveloperTools();
    builder.Logging.AddDebug();
#endif

    builder.Services.AddScoped<LazyLoaderService>();

    builder.Services.AddTransient<CustomHeaderMessageHandlerDemo>(sp => new(new HttpClientHandler()));

    //builder.Services.AddScoped(sp =>
    //{
    //    var httpClient = new HttpClient(sp.GetRequiredService<CustomHeaderMessageHandlerDemo>()) { BaseAddress = new Uri(baseAddress) };

    //    return httpClient;
    //});

    builder.Services.AddHttpClient<GitHubService>(client =>
    {
      client.BaseAddress = new Uri("https://api.github.com/");
      client.DefaultRequestHeaders.Add("User-Agent", "ILoveDotNet");
    });

    builder.Services.AddScoped<AppState>();

    builder.Services.AddScoped<AppStateDemo>();

    builder.Services.AddScoped<TableOfContents>();

    builder.Services.AddScoped<Achievements>();

    builder.Services.AddScoped<Sitemaps>();

    builder.Services.AddTransient<AdSpaceService>();

    builder.Services.AddScoped<LazyAssemblyLoader>();

    builder.Services.AddHeadElementHelper();

    builder.Services.AddGoogleAnalytics("G-PY5ZL88NSM");

    builder.Services.AddGoogleAdSense("ca-pub-1536083653226834");

    builder.Services.AddTransient<TransientServiceDemo>();

    builder.Services.AddScoped<ScopedServiceDemo>();

    builder.Services.AddSingleton<SingletonServiceDemo>();

    builder.Services.AddHotKeys2();

    builder.Services.AddSingleton<IHostEnvironment>(options =>
    {
      var hostEnvironment = new MAUIHostEnvironment();
#if DEBUG
      hostEnvironment.Environment = "Development";
#else
      hostEnvironment.Environment = "Production";
#endif
      return hostEnvironment;
    });

    builder.Services.AddScoped(serviceProvider => new SlugService(serviceProvider.GetRequiredService<NavigationManager>()));

    Func<OidcClientOptions, HttpClient> httpClientFactory = null;

#if DEBUG
    httpClientFactory = (options) =>
    {
        var handler = new HttpsClientHandlerService();
        return new HttpClient(handler.GetPlatformMessageHandler());
    };
#endif

    builder.Services.AddSingleton(new OidcClient(new()
    {
        Authority = "https://demo.duendesoftware.com",

        ClientId = "interactive.public",
        Scope = "openid profile email api",
        RedirectUri = "myapp://callback",

        Browser = new MauiAuthenticationBrowser(),

        HttpClientFactory = httpClientFactory,
    }));

    builder.Services.AddOptions();
	  builder.Services.AddAuthorizationCore();
    builder.Services.AddCascadingAuthenticationState();

    // workaround for using persistent state in MAUI
    //https://github.com/dotnet/aspnetcore/issues/43833
    builder.Services.AddSingleton<ComponentStatePersistenceManager>();
    builder.Services.AddSingleton<PersistentComponentState>(sp => sp.GetRequiredService<ComponentStatePersistenceManager>().State);

    return builder.Build();
  }
}
