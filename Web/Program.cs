using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json.Serialization;
using Web;
using Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AppState>();

builder.Services.AddBlazoredLocalStorage(configuration => 
{
    configuration.JsonSerializerOptions.WriteIndented = true;
    configuration.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

await builder.Build().RunAsync();
