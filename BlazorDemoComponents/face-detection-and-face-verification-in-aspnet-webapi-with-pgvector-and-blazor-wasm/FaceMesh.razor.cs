using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BlazorDemoComponents;

public partial class FaceMesh : IAsyncDisposable
{
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private ILogger<FaceMesh> Logger { get; set; } = default!;

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

  public async Task SetLivenessMode(string mode)
  {
    if (_initialized && _jsModule != null)
    {
      try
      {
        await _jsModule.InvokeVoidAsync("setLivenessMode", mode);
      }
      catch (JSException exception)
      {
        Logger.LogWarning(exception, "Unable to set face liveness mode");
      }
    }
  }

  public async Task ResetLiveness()
  {
    if (_initialized && _jsModule != null)
    {
      try
      {
        await _jsModule.InvokeVoidAsync("resetLiveness");
      }
      catch (JSException exception)
      {
        Logger.LogWarning(exception, "Unable to reset face liveness");
      }
    }
  }

  public bool WebcamEnabled { get; private set; }

  protected override async Task OnInitializedAsync()
  {
    try
    {
      _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
          "/face-detection-and-face-verification-in-aspnet-webapi-with-pgvector-and-blazor-wasm/FaceMesh.razor.js");
    }
    catch (JSException exception)
    {
      Logger.LogError(exception, "Unable to initialize face mesh interop");
    }

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
    }
    catch (JSException exception) when (exception.Message.Contains("NotAllowedError") || exception.Message.Contains("Permission denied"))
    {
      Logger.LogInformation(exception, "Camera permission denied; the user can retry");
    }
    catch (JSException exception)
    {
      Logger.LogError(exception, "Unable to start face mesh camera");
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
    try { await _jsModule.InvokeVoidAsync("dispose"); }
    catch (JSException exception) { Logger.LogWarning(exception, "Unable to stop face mesh camera"); }
  }

  public async Task CaptureFeatureVector()
  {
    if (_initialized && !IsCapturing && _jsModule != null)
    {
      IsCapturing = true;
      try
      {
        await _jsModule.InvokeVoidAsync("captureFeatureVector", _dotNetRef);
      }
      catch (JSException exception)
      {
        IsCapturing = false;
        Logger.LogError(exception, "Unable to capture face feature vector");
      }
      // IsCapturing is reset inside OnFeatureVectorReady after a successful capture.
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
      catch (JSException exception) { Logger.LogWarning(exception, "Unable to dispose face mesh interop"); }
      _dotNetRef?.Dispose();
    }
  }

  [JSInvokable]
  public void OnResults(string json)
  {
    DetectionResult = JsonSerializer.Deserialize(json, FaceMeshJsonContext.Default.FaceMeshResult);
    StateHasChanged();
  }

  [JSInvokable]
  public void OnFeatureVectorReady(string json)
  {
    IsCapturing = false;
    LatestVector = JsonSerializer.Deserialize(json, FaceMeshJsonContext.Default.FeatureVectorResult);
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
  public bool Detected { get; set; }
  public float[]? Vector { get; set; }
  public int SampleCount { get; set; }
}

public record FaceMeshResult
{
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
