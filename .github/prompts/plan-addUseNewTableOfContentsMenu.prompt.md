Add `UseNewTableOfContentsMenu=true` to the `<Content` tag in all blog post Razor files that are missing it.

## Completed

### LINQDemoComponents (25 files) ✅
All 25 LINQ blog pages have been fully restructured with `UseNewTableOfContentsMenu=true` and the `<What>`, `<Why>`, `<How>`, `<Summary>` layout.

### BlazorDemoComponents (27 files) ✅
All 27 Blazor WASM blog pages have been fully restructured with `UseNewTableOfContentsMenu=true` and the `<What>`, `<Why>`, `<How>`, `<Summary>` layout. TOC blocks removed, typos fixed, heading levels demoted in How sections.

## Files to Update (0 remaining)

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
