using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedModels;

namespace Web.Services;

internal class WebHostEnvironment(IWebAssemblyHostEnvironment webAssemblyHostEnvironment) : IHostEnvironment
{
    public bool IsDevelopment()
    {
        return webAssemblyHostEnvironment.IsDevelopment();
    }

    public bool IsProduction()
    {
        return webAssemblyHostEnvironment.IsProduction();
    }
}
