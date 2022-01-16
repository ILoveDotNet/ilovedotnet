using Microsoft.AspNetCore.Components;

namespace Web.Components;

public class DemoSnippetBase : ComponentBase
{
    [Parameter, EditorRequired] public string Title { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment DemoContent { get; set; } = default!;
}
