using Microsoft.AspNetCore.Components;

namespace BaseComponents;

public class DemoSnippetBase : ComponentBase
{
    [Parameter, EditorRequired] public string Title { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;
}
