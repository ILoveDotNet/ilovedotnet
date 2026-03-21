Add `UseNewTableOfContentsMenu=true` to the `<Content` tag in all blog post Razor files that are missing it.

## Completed

### LINQDemoComponents (25 files) ✅
All 25 LINQ blog pages have been fully restructured with `UseNewTableOfContentsMenu=true` and the `<What>`, `<Why>`, `<How>`, `<Summary>` layout.

## Files to Update (27 remaining)

### BlazorDemoComponents (27 files)
- BlazorDemoComponents/Pages/Blogs/Wasm/AppSettings.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/BlazorComponents.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/BlazorDynamicComponent.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/BlazorVirtualization.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/CommunicationBetweenComponents.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/ControllingHeadContent.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/DarkThemeAndLightTheme.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/DataBinding.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/Dockerizing.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/ErrorLogging.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/EventHandlingAndEventArguments.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/ExceptionHandlingAndErrorBoundary.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/Forms.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/GeneratingBarcode.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/HotKeys.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/HybridAppWithMAUI.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/Introduction.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/JavascriptInteropAndIsolation.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/LazyLoading.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PreRendering.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PrintingBarcode.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PublishingToAWSAmplify.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PublishingToAzureStaticWebApps.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PublishingToGitHubPages.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/PublishingToIIS.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/StylesAndCssIsolation.razor
- BlazorDemoComponents/Pages/Blogs/Wasm/Validation.razor

## Change Required

In each file, find the `<Content` opening tag and add `UseNewTableOfContentsMenu=true` as an attribute. The existing pattern looks like:

```razor
<Content FileName=@nameof(ComponentName)>
```

It should become:

```razor
<Content FileName=@nameof(ComponentName) UseNewTableOfContentsMenu=true>
```

Some files may have the attribute on a separate line or with additional attributes — preserve the existing formatting style.
