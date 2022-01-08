using Microsoft.AspNetCore.Components;

namespace Web.Shared;

public class AppRootBase : ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}
