namespace VectorSearchIndexGenerator;

internal static class ModelDownloader
{
  public static async Task DownloadIfMissingAsync(string url, string path)
  {
    if (File.Exists(path))
    {
      return;
    }

    Console.WriteLine($"Downloading {url}...");
    var temporaryPath = $"{path}.tmp";
    using var httpClient = new HttpClient();
    await using var source = await httpClient.GetStreamAsync(url);
    await using (var destination = File.Create(temporaryPath))
    {
      await source.CopyToAsync(destination);
      await destination.FlushAsync();
    }

    File.Move(temporaryPath, path, overwrite: true);
  }
}
