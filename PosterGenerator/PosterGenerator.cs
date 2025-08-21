using SkiaSharp;
using SharedModels;

namespace PosterGenerator;

public class PosterGenerator
{
    private readonly TableOfContents _tableOfContents;
    private readonly string _channel;
    private readonly string _outputPath;
    
    // Poster dimensions optimized for social sharing
    private const int PosterWidth = 1200;
    private const int PosterHeight = 630;
    
    // Brand colors
    private readonly SKColor _violetBackground = SKColor.Parse("#512bd4");
    private readonly SKColor _whiteText = SKColors.White;
    
    public PosterGenerator(TableOfContents tableOfContents, string channel, string outputPath)
    {
        _tableOfContents = tableOfContents;
        _channel = channel;
        _outputPath = outputPath;
    }

    public void GeneratePosters()
    {
        var channelContents = _tableOfContents
            .AllContents
            .Where(content => content.Channel.Equals(_channel, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.WriteLine($"Generating posters for {channelContents.Count} articles in {_channel} channel...");

        // Ensure output directory exists
        Directory.CreateDirectory(_outputPath);

        // Load logo once and reuse
        using var logo = LoadLogo();
        
        foreach (var content in channelContents)
        {
            var posterPath = Path.Combine(_outputPath, $"{content.Slug}.png");
            
            // Skip if poster already exists and content hasn't been modified
            if (ShouldSkipGeneration(posterPath, content))
            {
                Console.WriteLine($"Skipping {content.Slug} - poster up to date");
                continue;
            }

            try
            {
                GeneratePoster(content, posterPath, logo);
                Console.WriteLine($"Generated poster: {content.Slug}.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating poster for {content.Slug}: {ex.Message}");
            }
        }

        Console.WriteLine($"Poster generation completed for {_channel} channel.");
    }

    private bool ShouldSkipGeneration(string posterPath, ContentMetaData content)
    {
        if (!File.Exists(posterPath))
            return false;

        var posterLastWrite = File.GetLastWriteTime(posterPath);
        return posterLastWrite >= content.ModifiedOn;
    }

    private void GeneratePoster(ContentMetaData content, string outputPath, SKBitmap? logo)
    {
        using var surface = SKSurface.Create(new SKImageInfo(PosterWidth, PosterHeight));
        var canvas = surface.Canvas;

        // Clear with violet background
        canvas.Clear(_violetBackground);

        // Draw logo in top-left
        if (logo != null)
        {
            var logoSize = 80f;
            var logoRect = SKRect.Create(40, 40, logoSize, logoSize);
            canvas.DrawBitmap(logo, logoRect);
        }

        // Draw channel name in top-right
        DrawChannelName(canvas, content.Channel);

        // Draw website brand in top-right corner
        DrawWebsiteBrand(canvas);

        // Draw title in center
        DrawTitle(canvas, content.Title);

        // Save as PNG
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 90);
        using var stream = File.OpenWrite(outputPath);
        data.SaveTo(stream);
    }

    private void DrawChannelName(SKCanvas canvas, string channel)
    {
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 32f);
        using var paint = new SKPaint
        {
            Color = _whiteText,
            IsAntialias = true
        };

        var bounds = new SKRect();
        font.MeasureText(channel, out bounds, paint);
        
        var x = PosterWidth - bounds.Width - 40;
        var y = 40 + Math.Abs(bounds.Top);
        
        canvas.DrawText(channel, x, y, font, paint);
    }

    private void DrawWebsiteBrand(SKCanvas canvas)
    {
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), 24f);
        using var paint = new SKPaint
        {
            Color = _whiteText,
            IsAntialias = true
        };

        var brandText = "I ❤️ .NET";
        var bounds = new SKRect();
        font.MeasureText(brandText, out bounds, paint);
        
        var x = PosterWidth - bounds.Width - 40;
        var y = 90 + Math.Abs(bounds.Top);
        
        canvas.DrawText(brandText, x, y, font, paint);
    }

    private void DrawTitle(SKCanvas canvas, string title)
    {
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 48f);
        using var paint = new SKPaint
        {
            Color = _whiteText,
            IsAntialias = true
        };

        // Calculate available width for text (with margins)
        var availableWidth = PosterWidth - 80f; // 40px margin on each side
        var maxLines = 4;
        var lineHeight = 60f;

        var wrappedLines = WrapText(title, font, paint, availableWidth, maxLines);
        
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

    private List<string> WrapText(string text, SKFont font, SKPaint paint, float maxWidth, int maxLines)
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
            lines[lines.Count - 1] = lines[lines.Count - 1].TrimEnd() + "...";
        }

        return lines;
    }

    private SKBitmap? LoadLogo()
    {
        // Try multiple possible logo paths
        var possibleLogoPaths = new[]
        {
            // Path when running from build process
            Path.Combine(
                Path.GetDirectoryName(_outputPath) ?? "",
                "..", "..", "..", "CommonComponents", "wwwroot", "image", "brand", "mini-logo.png"),
            
            // Path when running from project root
            Path.Combine("CommonComponents", "wwwroot", "image", "brand", "mini-logo.png"),
            
            // Path relative to PosterGenerator project
            Path.Combine("..", "CommonComponents", "wwwroot", "image", "brand", "mini-logo.png")
        };

        foreach (var logoPath in possibleLogoPaths)
        {
            if (File.Exists(logoPath))
            {
                try
                {
                    Console.WriteLine($"Loading logo from: {logoPath}");
                    return SKBitmap.Decode(logoPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not load logo from {logoPath}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Logo not found in any of the expected locations");
        return null;
    }
}
