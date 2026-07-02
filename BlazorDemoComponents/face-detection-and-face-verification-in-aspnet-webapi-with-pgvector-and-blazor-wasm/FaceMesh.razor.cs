using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorDemoComponents;

public partial class FaceMesh : IAsyncDisposable
{
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

  private bool _initialized;
  private IJSObjectReference? _jsModule;
  private DotNetObjectReference<FaceMesh>? _dotNetRef;

  [Parameter] public string Mode { get; set; } = "visualize";
  [Parameter] public string LivenessMode { get; set; } = "none";
  [Parameter] public EventCallback<FeatureVectorResult> OnFeatureVectorCaptured { get; set; }

  public bool LivenessConfirmed { get; set; }
  public bool DepthConfirmed { get; set; }     // sub-flag for 'both' mode
  public bool FaceDetected { get; set; }
  public bool IsCapturing { get; private set; }
  public FeatureVectorResult? LatestVector { get; set; }

  public void SetLivenessMode(string mode)
  {
    if (_initialized && _jsModule != null)
      _ = _jsModule.InvokeVoidAsync("setLivenessMode", mode);
  }

  public void ResetLiveness()
  {
    if (_initialized && _jsModule != null)
      _ = _jsModule.InvokeVoidAsync("resetLiveness");
  }

  public bool WebcamEnabled { get; private set; }

  protected override async Task OnInitializedAsync()
  {
    _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
        "/face-detection-and-face-verification-in-aspnet-webapi-with-pgvector-and-blazor-wasm/FaceMesh.razor.js");
    _dotNetRef = DotNetObjectReference.Create(this);
    // Camera is NOT started automatically — user must click Enable Camera.
  }

  public async Task StartCamera()
  {
    if (_jsModule is null || _initialized) return;
    try
    {
      await _jsModule.InvokeVoidAsync("onInit", _dotNetRef, Mode, LivenessMode);
      _initialized = true;
      WebcamEnabled = true;
      StateHasChanged();
    }
    catch (Exception ex) when (ex.Message.Contains("NotAllowedError") || ex.Message.Contains("Permission denied"))
    {
      // Camera permission denied — user can retry.
    }
  }

  public async Task StopCamera()
  {
    if (_jsModule is null || !_initialized) return;
    _initialized = false;
    WebcamEnabled = false;
    FaceDetected = false;
    LivenessConfirmed = false;
    DepthConfirmed = false;
    CurrentVector = null;
    IsCapturing = false;
    StateHasChanged();
    try { await _jsModule.InvokeVoidAsync("dispose"); }
    catch (Exception ex) { Console.WriteLine($"FaceMesh.StopCamera: {ex.Message}"); }
  }

  public async Task CaptureFeatureVector()
  {
    if (_initialized && !IsCapturing && _jsModule != null)
    {
      IsCapturing = true;
      StateHasChanged();
      await _jsModule.InvokeVoidAsync("captureFeatureVector", _dotNetRef);
      // IsCapturing is reset inside OnFeatureVectorReady
    }
  }

  public async ValueTask DisposeAsync()
  {
    if (_jsModule != null)
    {
      try
      {
        if (_initialized)
        {
          _initialized = false;
          await _jsModule.InvokeVoidAsync("dispose");
        }
        await _jsModule.DisposeAsync();
      }
      catch (Exception ex) { Console.WriteLine($"FaceMesh.DisposeAsync: {ex.Message}"); }
      _dotNetRef?.Dispose();
    }
  }

  [JSInvokable]
  public void OnResults(string json)
  {
    DetectionResult = JsonSerializer.Deserialize<FaceMeshResult>(json, FaceMeshResult.SerializeOptions);
    StateHasChanged();
  }

  [JSInvokable]
  public void OnFeatureVectorReady(string json)
  {
    IsCapturing = false;
    LatestVector = JsonSerializer.Deserialize<FeatureVectorResult>(json, FeatureVectorResult.SerializeOptions);
    _ = OnFeatureVectorCaptured.InvokeAsync(LatestVector!);
    StateHasChanged();
  }

  [JSInvokable]
  public void OnDepthChanged(bool confirmed)
  {
    if (DepthConfirmed != confirmed)
    {
      DepthConfirmed = confirmed;
      StateHasChanged();
    }
  }

  [JSInvokable]
  public void OnLivenessChanged(bool confirmed)
  {
    if (LivenessConfirmed != confirmed)
    {
      LivenessConfirmed = confirmed;
      StateHasChanged();
    }
  }

  [JSInvokable]
  public void OnFaceStatusChanged(bool faceDetected)
  {
    if (FaceDetected != faceDetected)
    {
      FaceDetected = faceDetected;
      StateHasChanged();
    }
  }

  public float[]? CurrentVector { get; private set; }

  [JSInvokable]
  public void OnCurrentVectorUpdated(float[] vector)
  {
    CurrentVector = vector;
    StateHasChanged();
  }
}

public record FeatureVectorResult
{
  internal static readonly JsonSerializerOptions SerializeOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  };

  public bool Detected { get; set; }
  public float[]? Vector { get; set; }
  public int SampleCount { get; set; }
}

public record FaceMeshResult
{
  internal static readonly JsonSerializerOptions SerializeOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false
  };

  public List<Face> Faces { get; set; } = [];
}

public record Face
{
  public int Index { get; set; }
  public List<Landmark> Landmarks { get; set; } = [];
}

public record Landmark
{
  public double X { get; set; }
  public double Y { get; set; }
  public double Z { get; set; }
}
