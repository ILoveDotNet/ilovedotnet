namespace SharedModels;

public class BlazorLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(26);

    public BlazorLearningPath()
    {
        FullContents =
        new(26)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Blazor WASM Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.webp",
                ContentUrl = "blogs/blazor-wasm-introduction",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 3, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 3, 20, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Blazor WASM Components",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-components.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-components.webp",
                ContentUrl = "blogs/blazor-wasm-components",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 10, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 10, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Blazor WASM Data Binding",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.webp",
                ContentUrl = "blogs/blazor-wasm-data-binding",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 24, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 24, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Blazor WASM Event Handling And Event Arguments",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.webp",
                ContentUrl = "blogs/blazor-wasm-event-handling-and-event-arguments",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 17, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 17, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Blazor WASM Communication Between Components",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.webp",
                ContentUrl = "blogs/blazor-wasm-communication-between-components",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 22, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 22, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Blazor WASM Forms",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.webp",
                ContentUrl = "blogs/blazor-wasm-forms",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 29, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 29, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Blazor WASM Forms Validation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.webp",
                ContentUrl = "blogs/blazor-wasm-forms-validation",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 6, 5, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 5, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Blazor WASM Controlling Head Content",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.webp",
                ContentUrl = "blogs/blazor-wasm-controlling-head-content",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 8, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 8, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Blazor WASM Styles and CSS Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.webp",
                ContentUrl = "blogs/blazor-wasm-styles-and-css-isolation",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 13, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 19, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Blazor WASM Javascript Interop and Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.webp",
                ContentUrl = "blogs/blazor-wasm-javascript-interop-and-isolation",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 6, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 2, 6, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Blazor WASM Exception Handling and Error Boundary",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.webp",
                ContentUrl = "blogs/blazor-wasm-exception-handling-and-error-boundary",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 16, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 1, 23, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Blazor WASM Error Logging",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.webp",
                ContentUrl = "blogs/blazor-wasm-error-logging",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 10, 1, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 10, 1, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Blazor WASM Lazy Loading",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.webp",
                ContentUrl = "blogs/blazor-wasm-lazy-loading",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 29, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Blazor WASM Dark Theme and Light Theme",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.webp",
                ContentUrl = "blogs/blazor-wasm-dark-theme-and-light-theme",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 30, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 1, 30, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 15,
                Title = "Blazor WASM App Settings",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.webp",
                ContentUrl = "blogs/blazor-wasm-app-settings",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 27, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 2, 27, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 16,
                Title = "Blazor WASM Virtualization",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.webp",
                ContentUrl = "blogs/blazor-wasm-virtualization",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 6, 12, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 12, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 17,
                Title = "Blazor WASM Dynamic Component",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.webp",
                ContentUrl = "blogs/blazor-wasm-dynamic-component",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 3, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 3, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 18,
                Title = "Blazor WASM Publishing to IIS",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-iis",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 10, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 10, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 19,
                Title = "Blazor WASM Publishing to GitHub Pages",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-github-pages",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 17, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 17, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 20,
                Title = "Blazor WASM Pre Rendering",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.webp",
                ContentUrl = "blogs/blazor-wasm-pre-rendering",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 8, 7, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 7, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 21,
                Title = "Blazor WASM Dockerizing",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.webp",
                ContentUrl = "blogs/blazor-wasm-dockerizing",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 3, 26, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 26, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 22,
                Title = "Blazor WASM Publishing to AWS Amplify",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-aws-amplify",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 5, 21, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 5, 21, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 23,
                Title = "How to generate barcode in Blazor WASM",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/how-to-generate-barcode-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/how-to-generate-barcode-in-blazor-wasm.webp",
                ContentUrl = "blogs/how-to-generate-barcode-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 6, 25, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 6, 25, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 24,
                Title = "Printing barcode to label printer from Blazor WASM",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/printing-barcode-to-label-printer-from-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/printing-barcode-to-label-printer-from-blazor-wasm.webp",
                ContentUrl = "blogs/printing-barcode-to-label-printer-from-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 7, 2, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 7, 2, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 25,
                Title = "Improve performance by dynamically loading image in Blazor WASM",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/improve-performance-by-dynamically-loading-image-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/improve-performance-by-dynamically-loading-image-in-blazor-wasm.webp",
                ContentUrl = "blogs/improve-performance-by-dynamically-loading-image-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 10, 8, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 10, 8, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 26,
                Title = "Prevent image leech by dynamically streaming image in Blazor WASM",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm.webp",
                ContentUrl = "blogs/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 10, 15, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 10, 15, 22, 30, 0)
            }
        };
    }
}