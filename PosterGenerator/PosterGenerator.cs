using SkiaSharp;
using SharedModels;

namespace PosterGenerator;

public class PosterGenerator(TableOfContents tableOfContents,
                             string channel,
                             string outputPath)
{
  // Poster dimensions optimized for social sharing
  private const int PosterWidth = 1200;
  private const int PosterHeight = 630;

  // Brand colors for gradient
  private readonly SKColor _blueStart = SKColor.Parse("#0b6cff");   // Blue start color
  private readonly SKColor _violetEnd = SKColor.Parse("#512bd4");   // Violet end color
  private readonly SKColor _whiteText = SKColors.White;

  public void GeneratePosters()
  {
    var channelContents = tableOfContents
        .AllContents
        .Where(content => content.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase))
        .ToList();

    Console.WriteLine($"Generating posters for {channelContents.Count} articles in {channel} channel...");

    // Ensure output directory exists
    Directory.CreateDirectory(outputPath);

    foreach (var content in channelContents)
    {
      var posterPath = Path.Combine(outputPath, $"{content.Slug}.png");

      // Skip if poster already exists and content hasn't been modified
      if (ShouldSkipGeneration(posterPath, content))
      {
        Console.WriteLine($"Skipping {content.Slug} - poster up to date");
        continue;
      }

      try
      {
        GeneratePoster(content, posterPath);
        Console.WriteLine($"Generated poster: {content.Slug}.png");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error generating poster for {content.Slug}: {ex.Message}");
      }
    }

    Console.WriteLine($"Poster generation completed for {channel} channel.");
  }

  private static bool ShouldSkipGeneration(string posterPath, ContentMetaData content)
  {
    var webpPath = Path.ChangeExtension(posterPath, ".webp");

    // Check if both PNG and WebP files exist
    if (!File.Exists(posterPath) || !File.Exists(webpPath))
      return false;

    // Check if both files are up to date
    var pngLastWrite = File.GetLastWriteTime(posterPath);
    var webpLastWrite = File.GetLastWriteTime(webpPath);
    var earliestFileTime = pngLastWrite < webpLastWrite ? pngLastWrite : webpLastWrite;

    return earliestFileTime >= content.ModifiedOn;
  }

  private void GeneratePoster(ContentMetaData content, string outputPath)
  {
    // Create surface with RGB color type (no alpha) for better compression
    var imageInfo = new SKImageInfo(PosterWidth, PosterHeight, SKColorType.Rgb888x, SKAlphaType.Opaque);
    using var surface = SKSurface.Create(imageInfo);
    var canvas = surface.Canvas;

    // Create linear gradient background from left to right
    var gradientColors = new SKColor[] { _blueStart, _violetEnd };
    var gradientPositions = new float[] { 0.1405f, 0.893f }; // 14.05% and 89.3% positions
    using var gradientShader = SKShader.CreateLinearGradient(
        new SKPoint(0, 0),           // Start point (left)
        new SKPoint(PosterWidth, 0), // End point (right)
        gradientColors,
        gradientPositions,
        SKShaderTileMode.Clamp
    );

    using var gradientPaint = new SKPaint
    {
      Shader = gradientShader
    };

    // Fill background with gradient
    canvas.DrawRect(0, 0, PosterWidth, PosterHeight, gradientPaint);
    canvas.SetMatrix(SKMatrix.CreateIdentity()); // Ensure perfect pixel alignment

    // Logo removed for cleaner poster design

    // Draw channel name in top-right
    DrawChannelName(canvas, content.Channel);

    // Draw website brand in top-right corner
    DrawWebsiteBrand(canvas);

    // Draw title in center
    DrawTitle(canvas, content.Title);

    // Save as PNG and WebP with optimized compression
    using var image = surface.Snapshot();

    // Save PNG version with quality 50 (better balance for PNG)
    using var pngData = image.Encode(SKEncodedImageFormat.Png, 50);
    using var pngStream = File.OpenWrite(outputPath);
    pngData.SaveTo(pngStream);
    pngStream.Close();

    // Save WebP version with quality 25 (excellent compression)
    var webpPath = Path.ChangeExtension(outputPath, ".webp");
    using var webpData = image.Encode(SKEncodedImageFormat.Webp, 25);
    using var webpStream = File.OpenWrite(webpPath);
    webpData.SaveTo(webpStream);
    webpStream.Close();
  }

  private void DrawChannelName(SKCanvas canvas, string channel)
  {
    using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 72f);
    using var paint = new SKPaint
    {
      Color = _whiteText,
      IsAntialias = true
    };

    // Convert channel name to uppercase
    var upperCaseChannel = channel.ToUpperInvariant();

    var bounds = new SKRect();
    font.MeasureText(upperCaseChannel, out bounds, paint);

    var x = PosterWidth - bounds.Width - 40;
    var y = 40 + Math.Abs(bounds.Top);

    canvas.DrawText(upperCaseChannel, x, y, font, paint);
  }

  private void DrawWebsiteBrand(SKCanvas canvas)
  {
    using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), 24f);
    using var paint = new SKPaint
    {
      Color = _whiteText,
      IsAntialias = true
    };

    var brandText = "https://ilovedotnet.org";
    var bounds = new SKRect();
    font.MeasureText(brandText, out bounds, paint);

    // Position in bottom-left corner
    var x = 40f; // Left margin
    var y = PosterHeight - 40f; // Bottom margin

    canvas.DrawText(brandText, x, y, font, paint);
  }

  private void DrawTitle(SKCanvas canvas, string title)
  {
    using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 54f);
    using var paint = new SKPaint
    {
      Color = _whiteText,
      IsAntialias = true
    };

    // Calculate available width for text (with margins)
    var availableWidth = PosterWidth - 80f; // 40px margin on each side
    var maxLines = 4;
    var lineHeight = 68f;

    // Convert title to uppercase
    var upperCaseTitle = title.ToUpperInvariant();
    var wrappedLines = WrapText(upperCaseTitle, font, paint, availableWidth, maxLines);

    // Calculate total height of text block
    var totalTextHeight = wrappedLines.Count * lineHeight;

    // Center vertically
    var startY = (PosterHeight - totalTextHeight) / 2f + Math.Abs(font.Metrics.Top);

    for (int i = 0; i < wrappedLines.Count; i++)
    {
      var line = wrappedLines[i];
      var bounds = new SKRect();
      font.MeasureText(line, out bounds, paint);

      // Center horizontally
      var x = (PosterWidth - bounds.Width) / 2f;
      var y = startY + (i * lineHeight);

      canvas.DrawText(line, x, y, font, paint);
    }
  }

  private static List<string> WrapText(string text, SKFont font, SKPaint paint, float maxWidth, int maxLines)
  {
    var words = text.Split(' ');
    var lines = new List<string>();
    var currentLine = "";

    foreach (var word in words)
    {
      var testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
      var bounds = new SKRect();
      font.MeasureText(testLine, out bounds, paint);

      if (bounds.Width <= maxWidth)
      {
        currentLine = testLine;
      }
      else
      {
        if (!string.IsNullOrEmpty(currentLine))
        {
          lines.Add(currentLine);
          if (lines.Count >= maxLines)
            break;
        }
        currentLine = word;
      }
    }

    if (!string.IsNullOrEmpty(currentLine) && lines.Count < maxLines)
    {
      lines.Add(currentLine);
    }

    // If we exceeded max lines, add ellipsis to the last line
    if (lines.Count == maxLines && words.Length > lines.Sum(l => l.Split(' ').Length))
    {
      lines[^1] = lines[^1].TrimEnd() + "...";
    }

    return lines;
  }

}
