namespace SharedModels;

public class MLNETLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(4);
  public MLNETLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 3,
                Title = "AI Powered Language Detection in .NET with ML.NET and AutoML",
                Description = "In this post I will teach you detect language using ML.NET and AutoML in dotnet apps. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "ai-powered-language-detection-in-net-with-mlnet-and-automl",
                PosterUrl = "image/blogs/mlnet/ai-powered-language-detection-in-net-with-mlnet-and-automl.webp",
                ThumbnailUrl = "image/blogs/mlnet/ai-powered-language-detection-in-net-with-mlnet-and-automl.webp",
                ContentUrl = "blogs/ai-powered-language-detection-in-net-with-mlnet-and-automl",
                IconUrl = "image/icons/mlnet.webp",
                Channel = "MLNET",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 3, 9, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 3, 9, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Auto ML", "Machine Learning", "Language Detection", "AI", "Artificail Intelligence"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "AI Powered Image Recognition in .NET with ML.NET and ONNX Runtime",
                Description = "In this post I will teach you recognise image using ML.NET and onnx runtime in dotnet apps. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "ai-powered-image-recognition-in-net-with-mlnet-and-onnx-runtime",
                PosterUrl = "image/blogs/mlnet/ai-powered-image-recognition-in-net-with-mlnet-and-onnx-runtime.webp",
                ThumbnailUrl = "image/blogs/mlnet/ai-powered-image-recognition-in-net-with-mlnet-and-onnx-runtime.webp",
                ContentUrl = "blogs/ai-powered-image-recognition-in-net-with-mlnet-and-onnx-runtime",
                IconUrl = "image/icons/mlnet.webp",
                Channel = "MLNET",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 5, 11, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 5, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["ONNX", "Machine Learning", "Image Recognition", "AI", "Artificail Intelligence"]
            },
            new ContentMetaData
            {
                Order = 1,
                Title = "An Architectural View of ML.NET",
                Description = "In this post I will teach you the ML.NET architecture, how its learning pipeline works, and how to build machine learning models natively in .NET without Python.",
                Author = "Abdul Rahman",
                Slug = "an-architectural-view-of-mlnet",
                PosterUrl = "image/blogs/mlnet/an-architectural-view-of-mlnet.webp",
                ThumbnailUrl = "image/blogs/mlnet/an-architectural-view-of-mlnet.webp",
                ContentUrl = "blogs/an-architectural-view-of-mlnet",
                IconUrl = "image/icons/mlnet.webp",
                Channel = "MLNET",
                Type = "blogs",
                CreatedOn = new DateTime(2026, 7, 12, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 7, 12, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Machine Learning", "Pipeline", "MLContext", "IDataView", "ML.NET Architecture"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "The Fundamentals of ML.NET",
                Description = "In this post I will teach you the three ML roles, how to load and transform data with IDataView, apply OneHotEncoding and normalization, and train a regression model end-to-end using ML.NET in C#.",
                Author = "Abdul Rahman",
                Slug = "the-fundamentals-of-mlnet",
                PosterUrl = "image/blogs/mlnet/the-fundamentals-of-mlnet.webp",
                ThumbnailUrl = "image/blogs/mlnet/the-fundamentals-of-mlnet.webp",
                ContentUrl = "blogs/the-fundamentals-of-mlnet",
                IconUrl = "image/icons/mlnet.webp",
                Channel = "MLNET",
                Type = "blogs",
                CreatedOn = new DateTime(2026, 7, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 7, 19, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Machine Learning", "Data Engineering", "IDataView", "OneHotEncoding", "Regression", "ML.NET Training Pipeline"]
            }
    ];
  }
}
