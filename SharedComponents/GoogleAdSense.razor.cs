using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SharedModels;

namespace SharedComponents;

public class GoogleAdSenseBase : ComponentBase
{
  protected string Id = Guid.NewGuid().ToString();
  protected RenderFragment Ad { get; set; } = default!;

  [Inject] public IHostEnvironment HostEnvironment { get; set; } = default!;
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
                        new("class", "adsbygoogle"),
                        new("style", $"display:block;{Style}"),
                        new("data-ad-format", Format.ToString().ToLower()),
                        new("data-ad-client", GoogleAdSense.ClientId),
                        new("data-ad-layout-key", LayoutKey),
                        new("data-ad-slot", Slot)
                });
          break;
        case GoogleAdSenseAdType.Multiplex:
          b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
                {
                        new("class", "adsbygoogle"),
                        new("style", $"display:block;{Style}"),
                        new("data-ad-format", Format.ToString().ToLower()),
                        new("data-ad-client", GoogleAdSense.ClientId),
                        new("data-ad-slot", Slot)
                });
          break;
        case GoogleAdSenseAdType.InArticle:
          b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
                {
                        new("class", "adsbygoogle"),
                        new("style", $"display:block;{Style}"),
                        new("data-ad-layout", "in-article"),
                        new("data-ad-format", Format.ToString().ToLower()),
                        new("data-ad-client", GoogleAdSense.ClientId),
                        new("data-ad-slot", Slot)
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

public class GoogleAdSenseService(string clientId)
{
  public string ClientId { get; } = clientId;
}

public static class GoogleAdSenseServiceRegistration
{
  public static IServiceCollection AddGoogleAdSense(this IServiceCollection services, string clientId)
  {
    services.AddSingleton(new GoogleAdSenseService(clientId));
    return services;
  }
}
