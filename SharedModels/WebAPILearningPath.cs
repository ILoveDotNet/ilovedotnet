namespace SharedModels;

public class WebAPILearningPath
{
    public readonly List<ContentMetaData> FullContents = new(20);

    public WebAPILearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Importance of Status Code in Web API",
                Description = "In this post I will teach you the importance of status code in Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "webapi-importance-of-status-code",
                PosterUrl = "image/blogs/webapi/webapi-importance-of-status-code.webp",
                ThumbnailUrl = "image/blogs/webapi/webapi-importance-of-status-code.webp",
                ContentUrl = "blogs/webapi-importance-of-status-code",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 3, 6, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 3, 6, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Status Code"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Profiling Web API with Mini Profiler",
                Description = "In this post I will teach you how to profile Web API with Mini Profiler. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "profiling-webapi-with-mini-profiler",
                PosterUrl = "image/blogs/webapi/profiling-webapi-with-mini-profiler.webp",
                ThumbnailUrl = "image/blogs/webapi/profiling-webapi-with-mini-profiler.webp",
                ContentUrl = "blogs/profiling-webapi-with-mini-profiler",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 9, 18, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 4, 28, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Profiling", "Mini Profiler"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Unit Testing Filters in ASP.NET Web API",
                Description = "In this post I will teach how to unit test filters in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-filters-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/unit-testing-filters-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-filters-in-asp-net-webapi.webp",
                ContentUrl = "blogs/unit-testing-filters-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 5, 28, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 5, 28, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Test", "Filters"]
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Unit Testing Middlewares in ASP.NET Web API",
                Description = "In this post I will teach how to unit test middlewares in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-middlewares-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/unit-testing-middlewares-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-middlewares-in-asp-net-webapi.webp",
                ContentUrl = "blogs/unit-testing-middlewares-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 6, 4, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 6, 4, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Test", "Middlewares"]
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Unit Testing Service Registrations in ASP.NET Web API",
                Description = "In this post I will teach how to unit test service registrations in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-service-registrations-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/unit-testing-service-registrations-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-service-registrations-in-asp-net-webapi.webp",
                ContentUrl = "blogs/unit-testing-service-registrations-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 6, 11, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 6, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Test", "Service Registration"]
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Unit Testing Controllers in ASP.NET Web API",
                Description = "In this post I will teach how to unit test controllers in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-controllers-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/unit-testing-controllers-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-controllers-in-asp-net-webapi.webp",
                ContentUrl = "blogs/unit-testing-controllers-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 6, 18, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 6, 18, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Test", "Controllers"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Perform Background Workloads in Hosted Service using Channels in ASP.NET Web API",
                Description = "In this post I will teach how to perform background workloads outside request life cycle using channels in hosted services in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "perform-background-workloads-in-hosted-service-using-channels-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/perform-background-workloads-in-hosted-service-using-channels-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/perform-background-workloads-in-hosted-service-using-channels-in-asp-net-webapi.webp",
                ContentUrl = "blogs/perform-background-workloads-in-hosted-service-using-channels-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 7, 9, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 7, 9, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Hosted Service", "Background Workload", "Channels"]
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Functional testing your ASP.NET WEB API",
                Description = "In this post I will teach how to do functional testing in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "functional-testing-your-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/functional-testing-your-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/functional-testing-your-asp-net-webapi.webp",
                ContentUrl = "blogs/functional-testing-your-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 7, 16, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 6, 23, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Functional Test", "Integration Test"]
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Faking Dependencies in Functional testing in ASP.NET WEB API",
                Description = "In this post I will teach how to fake dependencies in functional testing in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "faking-dependencies-in-functional-testing-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/faking-dependencies-in-functional-testing-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/faking-dependencies-in-functional-testing-in-asp-net-webapi.webp",
                ContentUrl = "blogs/faking-dependencies-in-functional-testing-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 7, 23, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 6, 30, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Faking Dependencies", "Replace Services", "Functional Test", "Integration Test"]
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Using WireMock.NET in Functional testing in ASP.NET WEB API",
                Description = "In this post I will teach how to use wiremock.net in functional testing in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "using-wiremock-net-in-functional-testing-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/using-wiremock-net-in-functional-testing-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/using-wiremock-net-in-functional-testing-in-asp-net-webapi.webp",
                ContentUrl = "blogs/using-wiremock-net-in-functional-testing-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 7, 30, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 7, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["WireMock.Net", "Functional Test", "Integration Test"]
            },
            new ContentMetaData
            {
                Order = 15,
                Title = "Configuring Authentication in Functional testing in ASP.NET WEB API",
                Description = "In this post I will teach how to configure authentication in functional testing in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "configuring-authentication-in-functional-testing-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/configuring-authentication-in-functional-testing-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/configuring-authentication-in-functional-testing-in-asp-net-webapi.webp",
                ContentUrl = "blogs/configuring-authentication-in-functional-testing-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 8, 6, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 7, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Authentication", "Authorization", "Functional Test", "Integration Test"]
            },
            new ContentMetaData
            {
                Order = 16,
                Title = "Unit Testing Hosted Services in ASP.NET WEB API",
                Description = "In this post I will teach how to unit test hosted services in ASP.NET web api. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-hosted-services-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/unit-testing-hosted-services-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-hosted-services-in-asp-net-webapi.webp",
                ContentUrl = "blogs/unit-testing-hosted-services-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 8, 13, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 12, 15, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Test", "Hosted Service", "Background Service"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Request Endpoint Response (REPR) pattern in ASP.NET WEB API",
                Description = "In this post I will teach about REPR pattern, a better way to organize API's and how to implement in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "request-endpoint-response-repr-pattern-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/request-endpoint-response-repr-pattern-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/request-endpoint-response-repr-pattern-in-asp-net-webapi.webp",
                ContentUrl = "blogs/request-endpoint-response-repr-pattern-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 10, 29, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 10, 29, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Request", "Endpoint", "Response", "REPR"]
            },
            new ContentMetaData
            {
                Order = 17,
                Title = "Fitness Test using Net Arch Test in ASP.NET WEB API",
                Description = "In this post I will teach how to enforce architecture fitness using Fitness Test in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "fitness-test-using-net-arch-test-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/fitness-test-using-net-arch-test-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/fitness-test-using-net-arch-test-in-asp-net-webapi.webp",
                ContentUrl = "blogs/fitness-test-using-net-arch-test-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 11, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 12, 29, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Architecture Test", "Fitness Functions", "Fitness Test", "NetArchTest"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Global Exception Handling in ASP.NET WEB API",
                Description = "In this post I will teach how to handle exceptions globally in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "global-exception-handling-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/global-exception-handling-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/global-exception-handling-in-asp-net-webapi.webp",
                ContentUrl = "blogs/global-exception-handling-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 11, 12, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 11, 12, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Global", "Exception Handling", "Error Handling"]
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Using Docker Test Containers in Functional Testing in ASP.NET WEB API",
                Description = "In this post I will teach how to test with real database using Docker Test Containers in functional testing in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "using-docker-test-containers-in-functional-testing-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/using-docker-test-containers-in-functional-testing-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/using-docker-test-containers-in-functional-testing-in-asp-net-webapi.webp",
                ContentUrl = "blogs/using-docker-test-containers-in-functional-testing-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 11, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 7, 7, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Docker", "Test Containers", "Functional Testing"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Structured Logging with Serilog in ASP.NET WEB API",
                Description = "In this post I will teach how to use serilog to do structured logging in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "structured-logging-with-serilog-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/structured-logging-with-serilog-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/structured-logging-with-serilog-in-asp-net-webapi.webp",
                ContentUrl = "blogs/structured-logging-with-serilog-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 11, 26, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 11, 26, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Logging", "Serilog", "Structured Logging"]
            },
            new ContentMetaData
            {
                Order = 18,
                Title = "Implementing Caching using Decorator Pattern in ASP.NET WEB API",
                Description = "In this post I will teach how to implement caching using decorator pattern in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "implementing-caching-using-decorator-pattern-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/implementing-caching-using-decorator-pattern-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/implementing-caching-using-decorator-pattern-in-asp-net-webapi.webp",
                ContentUrl = "blogs/implementing-caching-using-decorator-pattern-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 1, 14, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 1, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Caching", "Decorator"]
            },
            new ContentMetaData
            {
                Order = 19,
                Title = "Implementing Health Checks in ASP.NET WEB API",
                Description = "In this post I will teach how to implement health checks in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "implementing-health-checks-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/implementing-health-checks-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/implementing-health-checks-in-asp-net-webapi.webp",
                ContentUrl = "blogs/implementing-health-checks-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 11, 3, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 11, 3, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Health Check", "Alive"]
            },
            new ContentMetaData
            {
                Order = 20,
                Title = "Using NBomber for Performance, Load and Stress testing in ASP.NET WEB API",
                Description = "In this post I will teach how to use nbomber for running performance test in ASP.NET Web API. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "using-nbomber-for-performance-load-and-stress-testing-in-asp-net-webapi",
                PosterUrl = "image/blogs/webapi/using-nbomber-for-performance-load-and-stress-testing-in-asp-net-webapi.webp",
                ThumbnailUrl = "image/blogs/webapi/using-nbomber-for-performance-load-and-stress-testing-in-asp-net-webapi.webp",
                ContentUrl = "blogs/using-nbomber-for-performance-load-and-stress-testing-in-asp-net-webapi",
                IconUrl = "image/icons/web api.webp",
                Channel = "Web API",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 1, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 1, 5, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Performance Test", "Load Test", "Stress Test", "NBomber"]
            },
        ];
    }
}