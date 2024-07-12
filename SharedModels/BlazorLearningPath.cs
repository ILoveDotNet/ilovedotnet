namespace SharedModels;

public class BlazorLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(27);

    public BlazorLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Blazor WASM Introduction",
                Description = "In this post I will introduce you to Blazor WASM - Single Page Application (SPA) from ASP.NET family. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-introduction",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-introduction.webp",
                ContentUrl = "blogs/blazor-wasm-introduction",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 3, 20, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 3, 20, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Blazor WASM", "SPA"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Blazor WASM Components",
                Description = "In this post I will teach what is components and how to use components in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-components",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-components.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-components.webp",
                ContentUrl = "blogs/blazor-wasm-components",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 4, 10, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 4, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Components"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Blazor WASM Data Binding",
                Description = "In this post I will teach what is data binding and how to pass data to components in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-data-binding",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-data-binding.webp",
                ContentUrl = "blogs/blazor-wasm-data-binding",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 4, 24, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 4, 24, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Data Binding", "One Way Binding", "Two Way Binding"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Blazor WASM Event Handling And Event Arguments",
                Description = "In this post I will teach how to handle events inside components in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-event-handling-and-event-arguments",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-event-handling-and-event-arguments.webp",
                ContentUrl = "blogs/blazor-wasm-event-handling-and-event-arguments",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 4, 17, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 3, 17, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Event Hadling", "Event Arguments"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Blazor WASM Communication Between Components",
                Description = "In this post I will teach how to communicate between components in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-communication-between-components",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-communication-between-components.webp",
                ContentUrl = "blogs/blazor-wasm-communication-between-components",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 5, 22, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 2, 4, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Communication", "Component Parameter", "Cascading Value", "Cascading Parameter", "Event Callback", "State Container"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Blazor WASM Forms",
                Description = "In this post I will teach how to use forms in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-forms",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms.webp",
                ContentUrl = "blogs/blazor-wasm-forms",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 5, 29, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 5, 29, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Forms", "Model", "EditContext", "EditForm"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Blazor WASM Forms Validation",
                Description = "In this post I will teach how to validate data collected in forms in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-forms-validation",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-forms-validation.webp",
                ContentUrl = "blogs/blazor-wasm-forms-validation",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 6, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 6, 5, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Forms", "Validation", "Fluent Validation"]
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Blazor WASM Controlling Head Content",
                Description = "In this post I will teach how to control head content in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-controlling-head-content",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-controlling-head-content.webp",
                ContentUrl = "blogs/blazor-wasm-controlling-head-content",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 5, 8, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 5, 8, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Head Outlet", "Page Title", "Head Content"]
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Blazor WASM Styles and CSS Isolation",
                Description = "In this post I will teach you what is CSS isolation and what factors to consider when styling your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-styles-and-css-isolation",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-styles-and-css-isolation.webp",
                ContentUrl = "blogs/blazor-wasm-styles-and-css-isolation",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 2, 13, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 19, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["CSS", "Styles", "CSS Isolation"]
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Blazor WASM Javascript Interop and Isolation",
                Description = "In this post I will teach you how to interact with javascript and isolate javascript in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-javascript-interop-and-isolation",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-javascript-interop-and-isolation.webp",
                ContentUrl = "blogs/blazor-wasm-javascript-interop-and-isolation",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 2, 6, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 12, 17, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Javascript", "Javascript Interop", "Javascript Isolation"]
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Blazor WASM Exception Handling and Error Boundary",
                Description = "In this post I will teach you how exception handling works and how to use error boundary in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-exception-handling-and-error-boundary",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-exception-handling-and-error-boundary.webp",
                ContentUrl = "blogs/blazor-wasm-exception-handling-and-error-boundary",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 1, 16, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 12, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Exception Handling", "Error Boundary"]
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Blazor WASM Error Logging",
                Description = "In this post I will teach you how to log error in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-error-logging",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-error-logging.webp",
                ContentUrl = "blogs/blazor-wasm-error-logging",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 1, 23, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 10, 1, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Exception Logging", "Error Logging", "SeriLog"]
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Blazor WASM Lazy Loading",
                Description = "In this post I will teach you how to lazy load assemblies in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-lazy-loading",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-lazy-loading.webp",
                ContentUrl = "blogs/blazor-wasm-lazy-loading",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 2, 20, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 12, 3, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Lazy Loading", "Micro Frontend"]
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Blazor WASM Dark Theme and Light Theme",
                Description = "In this post I will teach you how to toggle dark mode and light mode in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-dark-theme-and-light-theme",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dark-theme-and-light-theme.webp",
                ContentUrl = "blogs/blazor-wasm-dark-theme-and-light-theme",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 1, 30, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 1, 30, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Dark Mode", "Light Mode", "Dark Theme", "Light Theme"]
            },
            new ContentMetaData
            {
                Order = 15,
                Title = "Blazor WASM App Settings",
                Description = "In this post I will teach you how to read configuration from app settings in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-app-settings",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-app-settings.webp",
                ContentUrl = "blogs/blazor-wasm-app-settings",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 2, 27, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 12, 31, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["App Settings", "App Configuration"]
            },
            new ContentMetaData
            {
                Order = 16,
                Title = "Blazor WASM Virtualization",
                Description = "In this post I will teach how to use virtualization in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-virtualization",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-virtualization.webp",
                ContentUrl = "blogs/blazor-wasm-virtualization",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 6, 12, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 12, 24, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Virtualization", "Virtual DOM", "Performance"]
            },
            new ContentMetaData
            {
                Order = 17,
                Title = "Blazor WASM Dynamic Component",
                Description = "In this post I will teach what is dynamic component and how to use dynamic component in your Blazor applications. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-dynamic-component",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dynamic-component.webp",
                ContentUrl = "blogs/blazor-wasm-dynamic-component",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 7, 3, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 2, 18, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Dynamic", "Component", "Dynamically-rendered"]
            },
            new ContentMetaData
            {
                Order = 18,
                Title = "Blazor WASM Publishing to IIS",
                Description = "In this post I will teach how to publish your Blazor applications to IIS. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-publishing-to-iis",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-iis.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-iis",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 7, 10, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 3, 24, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Publishing", "IIS"]
            },
            new ContentMetaData
            {
                Order = 19,
                Title = "Blazor WASM Publishing to GitHub Pages",
                Description = "In this post I will teach how to publish your Blazor applications to GitHub Pages. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-publishing-to-github-pages",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-github-pages.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-github-pages",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 7, 17, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 3, 31, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Publishing", "GitHub Pages", "GitHub", "CI/CD", "Continuous Integration", "Continuous Deployment"]
            },
            new ContentMetaData
            {
                Order = 20,
                Title = "Blazor WASM Pre Rendering",
                Description = "In this post I will teach how to prerender Blazor applications during publish time to improve SEO. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-pre-rendering",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-pre-rendering.webp",
                ContentUrl = "blogs/blazor-wasm-pre-rendering",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 8, 7, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 3, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Pre Rendering", "SPA Prerendering", "Blazor WASM Prerendering", "Search Engine Optimization", "SEO", "Social Media Optimization", "SMO"]
            },
            new ContentMetaData
            {
                Order = 21,
                Title = "Blazor WASM Dockerizing",
                Description = "In this post I will teach you how to dockerize stand alone blazor wasm app in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-dockerizing",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-dockerizing.webp",
                ContentUrl = "blogs/blazor-wasm-dockerizing",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 3, 26, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 3, 26, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Docker", "Dockerizing", "Container", "Containerizing"]
            },
            new ContentMetaData
            {
                Order = 22,
                Title = "Blazor WASM Publishing to AWS Amplify",
                Description = "In this post I will teach how to publish your Blazor applications to AWS Amplify. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-wasm-publishing-to-aws-amplify",
                PosterUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/blazor-wasm-publishing-to-aws-amplify.webp",
                ContentUrl = "blogs/blazor-wasm-publishing-to-aws-amplify",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 5, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 5, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Publishing", "AWS Amplify", "AWS", "CI/CD", "Continuous Integration", "Continuous Deployment"]
            },
            new ContentMetaData
            {
                Order = 23,
                Title = "How to generate barcode in Blazor WASM",
                Description = "In this post I will teach how to generate barcode in blazor wasm application. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "how-to-generate-barcode-in-blazor-wasm",
                PosterUrl = "image/blogs/blazor/wasm/how-to-generate-barcode-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/how-to-generate-barcode-in-blazor-wasm.webp",
                ContentUrl = "blogs/how-to-generate-barcode-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 6, 25, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 6, 25, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Barcode", "bwip-js"]
            },
            new ContentMetaData
            {
                Order = 24,
                Title = "Printing barcode to label printer from Blazor WASM",
                Description = "In this post I will teach how to print barcode to label printer from blazor wasm application. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "printing-barcode-to-label-printer-from-blazor-wasm",
                PosterUrl = "image/blogs/blazor/wasm/printing-barcode-to-label-printer-from-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/printing-barcode-to-label-printer-from-blazor-wasm.webp",
                ContentUrl = "blogs/printing-barcode-to-label-printer-from-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 7, 2, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 7, 2, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Barcode", "bwip-js"]
            },
            new ContentMetaData
            {
                Order = 25,
                Title = "Improve performance by dynamically loading image in Blazor WASM",
                Description = "In this post I will teach how to improve performance by dynamically loading image in blazor wasm. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "improve-performance-by-dynamically-loading-image-in-blazor-wasm",
                PosterUrl = "image/blogs/blazor/wasm/improve-performance-by-dynamically-loading-image-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/improve-performance-by-dynamically-loading-image-in-blazor-wasm.webp",
                ContentUrl = "blogs/improve-performance-by-dynamically-loading-image-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 10, 8, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 10, 8, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Dynamic Image Load", "Performance"]
            },
            new ContentMetaData
            {
                Order = 26,
                Title = "Prevent image leech by dynamically streaming image in Blazor WASM",
                Description = "In this post I will teach how to prevent image leech by dynamically streaming image in blazor wasm. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm",
                PosterUrl = "image/blogs/blazor/wasm/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm.webp",
                ContentUrl = "blogs/prevent-image-leech-by-dynamically-streaming-image-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 10, 15, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 10, 15, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Image Leech", "Dynamic Image Stream"]
            },
            new ContentMetaData
            {
                Order = 27,
                Title = "Using Hot Keys in Blazor WASM",
                Description = "In this post I will teach how to setup and use hot keys in blazor wasm. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "using-hot-keys-in-blazor-wasm",
                PosterUrl = "image/blogs/blazor/wasm/using-hot-keys-in-blazor-wasm.webp",
                ThumbnailUrl = "image/blogs/blazor/wasm/using-hot-keys-in-blazor-wasm.webp",
                ContentUrl = "blogs/using-hot-keys-in-blazor-wasm",
                IconUrl = "image/icons/blazor.webp",
                Channel = "Blazor",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 10, 22, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 10, 22, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Keyboard", "Shortcut"]
            }
        ];
    }
}