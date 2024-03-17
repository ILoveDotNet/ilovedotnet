using Blazor.Analytics;
using CommonComponents.Models;
using CommonComponents.Services;
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

        builder.Services.AddTransient<CustomHeaderMessageHandlerDemo>(sp => new(new HttpClientHandler()));

        //builder.Services.AddScoped(sp =>
        //{
        //    var httpClient = new HttpClient(sp.GetRequiredService<CustomHeaderMessageHandlerDemo>()) { BaseAddress = new Uri(baseAddress) };

        //    return httpClient;
        //});

        builder.Services.AddHttpClient<GitHubService>(client => client.BaseAddress = new Uri("https://api.github.com/"));

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

        return builder.Build();
    }
}
