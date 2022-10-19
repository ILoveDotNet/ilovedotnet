using Microsoft.AspNetCore.Components;

namespace Components;

public class GoogleAdSenseBase : ComponentBase
{
    protected string Id = Guid.NewGuid().ToString();
    protected RenderFragment Ads { get; set; }

    [Parameter] public string Slot { get; set; } = "2741354693";
    [Parameter] public string Style { get; set; } = "display:block";
    [Parameter] public string Adsformat { get; set; } = "fluid";


    protected override void OnParametersSet()
    {
        Ads = new RenderFragment(b =>
        {
            b.OpenElement(0, "ins");
            b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>()
                {
                new KeyValuePair<string, object>("class", "adsbygoogle"),
                new KeyValuePair<string, object>("style", $"{Style}"),
                new KeyValuePair<string, object>("data-ad-format", Adsformat),
                new KeyValuePair<string, object>("data-ad-layout-key", "-7m+de+1z+n+3"),
                new KeyValuePair<string, object>("data-ad-client", "ca-pub-1536083653226834"),
                new KeyValuePair<string, object>("data-ad-slot", Slot)
                });
            b.CloseElement();

            b.OpenElement(0, "script");
            b.AddContent(3, "(adsbygoogle = window.adsbygoogle || []).push({});");
            b.CloseElement();
        });
    }
}