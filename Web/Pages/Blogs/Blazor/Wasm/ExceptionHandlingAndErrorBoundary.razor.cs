using Microsoft.AspNetCore.Components.Web;
using Web.Components;

namespace Web.Pages.Blogs.Blazor.Wasm;

public class ExceptionHandlingAndErrorBoundaryBase : FragmentNavigationBase
{
    protected ErrorBoundary errorBoundary = default!;

    protected void Recover()
    {
        errorBoundary?.Recover();
    }
}
