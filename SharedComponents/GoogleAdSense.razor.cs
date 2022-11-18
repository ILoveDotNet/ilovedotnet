using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SharedComponents;

public class GoogleAdSenseBase : ComponentBase
{
    protected string Id = Guid.NewGuid().ToString();
    protected RenderFragment Ad { get; set; } = default!;

    [Inject] public IWebAssemblyHostEnvironment HostEnvironment { get; set; } = default!;
    [Inject] public GoogleAdSenseService GoogleAdSense { get; set; } = default!;

    [Parameter, EditorRequired] public GoogleAdSenseAdType Type { get; set; }
    [Parameter, EditorRequired] public GoogleAdSenseAdFormat Format { get; set; }
    [Parameter, EditorRequired] public string Slot { get; set; } = default!;
    [Parameter] public string LayoutKey { get; set; } = default!;
    [Parameter] public string Style { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (Type is GoogleAdSenseAdType.InFeed) ArgumentException.ThrowIfNullOrEmpty(LayoutKey, nameof(LayoutKey));
        ArgumentException.ThrowIfNullOrEmpty(Slot, nameof(Slot));
    }

    protected override void OnParametersSet()
    {
        Ad = new RenderFragment(b =>
        {
            b.OpenElement(0, "ins");

            switch (Type)
            {
                case GoogleAdSenseAdType.InFeed:
                    b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("class", "adsbygoogle"),
                        new KeyValuePair<string, object>("style", $"display:block;{Style}"),
                        new KeyValuePair<string, object>("data-ad-format", Format),
                        new KeyValuePair<string, object>("data-ad-client", GoogleAdSense.ClientId),
                        new KeyValuePair<string, object>("data-ad-layout-key", LayoutKey),
                        new KeyValuePair<string, object>("data-ad-slot", Slot)
                    });
                    break;
                case GoogleAdSenseAdType.Multiplex:
                    b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("class", "adsbygoogle"),
                        new KeyValuePair<string, object>("style", $"display:block;{Style}"),
                        new KeyValuePair<string, object>("data-ad-format", Format),
                        new KeyValuePair<string, object>("data-ad-client", GoogleAdSense.ClientId),
                        new KeyValuePair<string, object>("data-ad-slot", Slot)
                    });
                    break;
                case GoogleAdSenseAdType.InArticle:
                    b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("class", "adsbygoogle"),
                        new KeyValuePair<string, object>("style", $"display:block;{Style}"),
                        new KeyValuePair<string, object>("data-ad-layout", "in-article"),
                        new KeyValuePair<string, object>("data-ad-format", Format),
                        new KeyValuePair<string, object>("data-ad-client", GoogleAdSense.ClientId),
                        new KeyValuePair<string, object>("data-ad-slot", Slot)
                    });
                    break;
                case GoogleAdSenseAdType.Display:
                    break;
            }

            b.CloseElement();
            b.OpenElement(0, "script");
            b.AddContent(3, "(adsbygoogle = window.adsbygoogle || []).push({});");
            b.CloseElement();
        });
    }
}

public enum GoogleAdSenseAdType
{
    Display,
    InFeed,
    InArticle,
    Multiplex
}

public enum GoogleAdSenseAdFormat
{
    Fluid,
    AutoRelaxed
}

public class GoogleAdSenseService 
{
    public GoogleAdSenseService(string clientId)
    {
        ClientId = clientId;
    }

    public string ClientId { get; }
}

public static class GoogleAdSenseServiceResgitration
{
    public static IServiceCollection AddGoogleAdSense(this IServiceCollection services, string clientId) 
    {
        services.AddSingleton(new GoogleAdSenseService(clientId));
        return services;
    }
}