namespace SharedModels;

public class BlazorLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(22);

    public BlazorLearningPath()
    {
        FullContents =
        new(22)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Blazor WASM Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.png",
                ContentUrl = "blogs/blazor-wasm-introduction",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 3, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 3, 20, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Blazor WASM Components",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-components.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-components.png",
                ContentUrl = "blogs/blazor-wasm-components",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 10, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 10, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Blazor WASM Data Binding",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.png",
                ContentUrl = "blogs/blazor-wasm-data-binding",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 24, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 24, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Blazor WASM Event Handling And Event Arguments",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.png",
                ContentUrl = "blogs/blazor-wasm-event-handling-and-event-arguments",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 4, 17, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 17, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Blazor WASM Communication Between Components",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.png",
                ContentUrl = "blogs/blazor-wasm-communication-between-components",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 22, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 22, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Blazor WASM Forms",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.png",
                ContentUrl = "blogs/blazor-wasm-forms",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 29, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 29, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Blazor WASM Forms Validation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.png",
                ContentUrl = "blogs/blazor-wasm-forms-validation",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 6, 5, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 5, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Blazor WASM Controlling Head Content",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.png",
                ContentUrl = "blogs/blazor-wasm-controlling-head-content",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 5, 8, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 8, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Blazor WASM Styles and CSS Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.png",
                ContentUrl = "blogs/blazor-wasm-styles-and-css-isolation",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 13, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 19, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Blazor WASM Javascript Interop and Isolation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.png",
                ContentUrl = "blogs/blazor-wasm-javascript-interop-and-isolation",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 6, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 2, 6, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Blazor WASM Exception Handling and Error Boundary",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.png",
                ContentUrl = "blogs/blazor-wasm-exception-handling-and-error-boundary",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 16, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 1, 23, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Blazor WASM Error Logging",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.png",
                ContentUrl = "blogs/blazor-wasm-error-logging",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 23, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 1, 23, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Blazor WASM Lazy Loading",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.png",
                ContentUrl = "blogs/blazor-wasm-lazy-loading",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 29, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Blazor WASM Dark Theme and Light Theme",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.png",
                ContentUrl = "blogs/blazor-wasm-dark-theme-and-light-theme",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 1, 30, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 1, 30, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 15,
                Title = "Blazor WASM App Settings",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.png",
                ContentUrl = "blogs/blazor-wasm-app-settings",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 2, 27, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 2, 27, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 16,
                Title = "Blazor WASM Virtualization",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.png",
                ContentUrl = "blogs/blazor-wasm-virtualization",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 6, 12, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 12, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 17,
                Title = "Blazor WASM Dynamic Component",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.png",
                ContentUrl = "blogs/blazor-wasm-dynamic-component",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 3, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 3, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 18,
                Title = "Blazor WASM Publishing to IIS",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.png",
                ContentUrl = "blogs/blazor-wasm-publishing-to-iis",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 10, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 10, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 19,
                Title = "Blazor WASM Publishing to GitHub Pages",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.png",
                ContentUrl = "blogs/blazor-wasm-publishing-to-github-pages",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 7, 17, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 17, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 20,
                Title = "Blazor WASM Pre Rendering",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.png",
                ContentUrl = "blogs/blazor-wasm-pre-rendering",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2022, 8, 7, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 7, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 21,
                Title = "Blazor WASM Dockerizing",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.png",
                ContentUrl = "blogs/blazor-wasm-dockerizing",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 3, 26, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 26, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 22,
                Title = "Blazor WASM Publishing to AWS Amplify",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.png",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.png",
                ContentUrl = "blogs/blazor-wasm-publishing-to-aws-amplify",
                IconUrl = "image/icons/blazor.png",
                Type = "Blazor",
                CreatedOn = new DateTime(2023, 5, 21, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 5, 21, 22, 30, 0)
            }
        };
    }
}