using Microsoft.AspNetCore.Components;
using SharedModels;

namespace BaseComponents;

public class DemoSnippetBase : ComponentBase
{
  [Inject] public IHostEnvironment Environment { get; set; } = default!;

  [Parameter, EditorRequired] public string Title { get; set; } = default!;
  [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;
}
