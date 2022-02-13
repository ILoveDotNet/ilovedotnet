namespace Web.Models;

public class TableOfContents
{
    public List<ContentMetaData> Contents { get; set; } =
        new(3)
        {
            new ContentMetaData
            {
                Title = "Blazor WASM Exception Handling and Error Boundary",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary-1200w.svg",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary-400w.svg",
                ContentUrl = "blogs/blazor-wasm-exception-handling-and-error-boundary",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 16, 22, 30, 0)
            },
            new ContentMetaData
            {
                Title = "Blazor WASM Error Logging",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging-1200w.svg",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging-400w.svg",
                ContentUrl = "blogs/blazor-wasm-error-logging",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 23, 22, 30, 0)
            },
            new ContentMetaData
            {
                Title = "Blazor WASM Dark Theme and Light Theme",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme-1200w.svg",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme-400w.svg",
                ContentUrl = "blogs/blazor-wasm-dark-theme-and-light-theme",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 30, 22, 30, 0)
            },
            new ContentMetaData
            {
                Title = "Blazor WASM Javascript Interop and Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation-1200w.svg",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation-400w.svg",
                ContentUrl = "blogs/blazor-wasm-javascript-interop-and-isolation",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 6, 22, 30, 0)
            },
            new ContentMetaData
            {
                Title = "Blazor WASM Styles and CSS Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation-1200w.svg",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation-400w.svg",
                ContentUrl = "blogs/blazor-wasm-styles-and-css-isolation",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 13, 22, 30, 0)
            }
        };
}
