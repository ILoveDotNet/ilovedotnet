using Microsoft.AspNetCore.Components;
using SharedModels;

namespace SharedComponents;

public class SponsorshipBase : ComponentBase
{
    [Parameter, EditorRequired] public SponsorMetaData Sponsor { get; set; } = default!;
}