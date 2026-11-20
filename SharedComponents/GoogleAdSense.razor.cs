using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using SharedModels;

namespace SharedComponents;

public class GoogleAdSenseBase : ComponentBase, IAsyncDisposable
{
  protected string Id = Guid.NewGuid().ToString();
  protected RenderFragment Ad { get; set; } = default!;
  protected bool InitializationFailed;
  private IJSObjectReference? module;
  private Task<IJSObjectReference>? moduleTask;
  private string? observedId;
  private bool disposed;
  private (GoogleAdSenseAdType, GoogleAdSenseAdFormat, string, string?)? unit;

  [Inject] public IHostEnvironment HostEnvironment { get; set; } = default!;
  [Inject] public GoogleAdSenseService GoogleAdSense { get; set; } = default!;
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private ILogger<GoogleAdSenseBase> Logger { get; set; } = default!;

  [Parameter, EditorRequired] public GoogleAdSenseAdType Type { get; set; }
  [Parameter, EditorRequired] public GoogleAdSenseAdFormat Format { get; set; }
  [Parameter, EditorRequired] public string Slot { get; set; } = default!;
  [Parameter] public string LayoutKey { get; set; } = default!;
  [Parameter] public string Style { get; set; } = default!;

  protected override void OnParametersSet()
  {
    if (Type is GoogleAdSenseAdType.InFeed) ArgumentException.ThrowIfNullOrEmpty(LayoutKey, nameof(LayoutKey));
    ArgumentException.ThrowIfNullOrEmpty(Slot, nameof(Slot));

    var nextUnit = (Type, Format, Slot, LayoutKey);
    if (unit != nextUnit)
    {
      if (unit.HasValue) Id = Guid.NewGuid().ToString();
      unit = nextUnit;
      InitializationFailed = false;
    }

    Ad = new RenderFragment(b =>
    {
      b.OpenElement(0, "ins");
      b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>
      {
        new("class", "ad-slot-pending"),
        new("style", $"display:block;{Style}"),
        new("data-ad-client", GoogleAdSense.ClientId),
        new("data-ad-slot", Slot),
        new("data-ad-format", Type == GoogleAdSenseAdType.Display ? "auto" : Format.ToString().ToLowerInvariant())
      });

      switch (Type)
      {
        case GoogleAdSenseAdType.InFeed:
          b.AddMultipleAttributes(2, new List<KeyValuePair<string, object>>
                {
                        new("data-ad-layout-key", LayoutKey)
                });
          break;
        case GoogleAdSenseAdType.Multiplex:
          break;
        case GoogleAdSenseAdType.InArticle:
          b.AddMultipleAttributes(2, new List<KeyValuePair<string, object>>
                {
                        new("data-ad-layout", "in-article")
                });
          break;
        case GoogleAdSenseAdType.Display:
          b.AddAttribute(2, "data-full-width-responsive", "true");
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(Type));
      }

      b.CloseElement();
    });
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (!HostEnvironment.IsProduction() || observedId == Id) return;
    var requestedId = Id;
    try
    {
      var imported = module ?? await (moduleTask ??=
          JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/ads.js").AsTask());
      if (disposed)
      {
        if (module is null)
        {
          module = imported;
          await imported.DisposeAsync();
        }
        return;
      }

      module = imported;
      if (requestedId != Id || observedId == requestedId) return;
      var previousId = observedId;
      observedId = requestedId;
      if (previousId is not null)
      {
        await module.InvokeVoidAsync("stopObservingAd", previousId);
      }
      if (disposed || requestedId != Id) return;
      await module.InvokeVoidAsync("observeAd", requestedId);
    }
    catch (JSDisconnectedException) when (disposed)
    {
      return;
    }
    catch (JSException exception)
    {
      Logger.LogWarning(exception, "Unable to initialise advertisement {Slot}", Slot);
      if (module is null) moduleTask = null;
      if (!disposed && requestedId == Id)
      {
        observedId = requestedId;
        InitializationFailed = true;
        await InvokeAsync(StateHasChanged);
      }
    }
  }

  public async ValueTask DisposeAsync()
  {
    disposed = true;
    if (module is null) return;
    try
    {
      if (observedId is not null)
      {
        await module.InvokeVoidAsync("stopObservingAd", observedId);
      }
      await module.DisposeAsync();
    }
    catch (JSDisconnectedException)
    {
      // The browser or native web view has already closed.
    }
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
