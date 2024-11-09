using Microsoft.AspNetCore.Components;
using SharedModels;

namespace SharedComponents;

public class SponsorBase : ComponentBase
{
    [Parameter, EditorRequired] public SponsorMetaData Data { get; set; } = default!;
}