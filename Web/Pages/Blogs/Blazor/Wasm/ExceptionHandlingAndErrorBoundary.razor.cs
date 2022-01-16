using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Web.Pages.Blogs.Blazor.Wasm;

public class ExceptionHandlingAndErrorBoundaryBase: ComponentBase
{
    protected ErrorBoundary errorBoundary = default!;

    protected void Recover() 
    {
        errorBoundary?.Recover();
    }
}
