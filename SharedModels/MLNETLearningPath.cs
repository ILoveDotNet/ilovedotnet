namespace SharedModels;
public class MLNETLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(2);
  public MLNETLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
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
                Order = 2,
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
            }
    ];
  }
}
