namespace Crackdownlike;

using System;
using Chickensoft.GoDotCollections;
using Godot;

public interface IGameRepo : IDisposable
{
  /// <summary>Event invoked when the game ends.</summary>
  event Action<GameOverReason>? Ended;

  /// <summary>Event invoked when a coin is collected.</summary>
  event Action? CoinCollected;

  /// <summary>Event invoked when a jumpshroom is used to bounce.</summary>
  event Action? JumpshroomUsed;

  /// <summary>Event invoked whenever the player jumps.</summary>
  event Action? Jumped;

  /// <summary>Event invoked when the game should be saved.</summary>
  event Action? SaveRequested;

  /// <summary>Event invoked when the game save is completed.</summary>
  event Action? SaveCompleted;

  /// <summary>Mouse captured status.</summary>
  IAutoProp<bool> IsMouseCaptured { get; }

  /// <summary>Pause status.</summary>
  IAutoProp<bool> IsPaused { get; }

  /// <summary>Number of coins collected by the players.</summary>
  IAutoProp<int> NumCoinsCollected { get; }

  /// <summary>The total number of coins the world started with.</summary>
  IAutoProp<int> NumCoinsAtStart { get; }

  /// <summary>Player's position in global coordinates.</summary>
  IAutoProp<Vector3> PlayerGlobalPosition { get; }

  /// <summary>Camera's global transform basis.</summary>
  IAutoProp<Basis> CameraBasis { get; }

  /// <summary>Camera's global forward direction vector.</summary>
  Vector3 GlobalCameraDirection { get; }

  /// <summary>Inform the game that a jumpshroom was used.</summary>
  void OnJumpshroomUsed();


  /// <summary>Tells the game how many coins the game world contains.</summary>
  /// <param name="numCoinsAtStart">Initial number of coins.</param>
  void SetNumCoinsAtStart(int numCoinsAtStart);

  /// <summary>Inform the game that the game ended.</summary>
  void OnGameEnded(GameOverReason reason);

  /// <summary>Pauses the game and releases the mouse.</summary>
  void Pause();

  /// <summary>Resumes the game and recaptures the mouse.</summary>
  void Resume();

  /// <summary>Tells the game that the player jumped.</summary>
  void OnJump();

  /// <summary>Starts the saving process.</summary>
  void Save();

  /// <summary>Changes whether the mouse is captured or not.</summary>
  /// <param name="isMouseCaptured">
  ///   Whether or not the mouse is captured.
  /// </param>
  void SetIsMouseCaptured(bool isMouseCaptured);

  /// <summary>Sets the camera's global transform basis.</summary>
  /// <param name="cameraBasis">Camera global transform basis.</param>
  void SetCameraBasis(Basis cameraBasis);

  /// <summary>Sets the player's global position.</summary>
  /// <param name="playerGlobalPosition">
  ///   Player's global position in world
  ///   coordinates.
  /// </param>
  void SetPlayerGlobalPosition(Vector3 playerGlobalPosition);
}

/// <summary>
///   Game repository — stores pure game logic that's not directly related to the
///   game node's overall view.
/// </summary>
public class GameRepo : IGameRepo
{
  public IAutoProp<bool> IsMouseCaptured => _isMouseCaptured;
  private readonly AutoProp<bool> _isMouseCaptured;
  public IAutoProp<bool> IsPaused => _isPaused;
  private readonly AutoProp<bool> _isPaused;
  public IAutoProp<Vector3> PlayerGlobalPosition => _playerGlobalPosition;
  private readonly AutoProp<Vector3> _playerGlobalPosition;

  public IAutoProp<Basis> CameraBasis => _cameraBasis;
  private readonly AutoProp<Basis> _cameraBasis;

  public Vector3 GlobalCameraDirection => -_cameraBasis.Value.Z;

  public IAutoProp<int> NumCoinsCollected => _numCoinsCollected;
  private readonly AutoProp<int> _numCoinsCollected;
  public IAutoProp<int> NumCoinsAtStart => _numCoinsAtStart;
  private readonly AutoProp<int> _numCoinsAtStart;
  public event Action? CoinCollected;
  public event Action? JumpshroomUsed;
  public event Action<GameOverReason>? Ended;
  public event Action? SaveRequested;
  public event Action? SaveCompleted;
  public event Action? Jumped;

  private int _coinsBeingCollected;
  private bool _disposedValue;

  public GameRepo()
  {
    _isMouseCaptured = new AutoProp<bool>(false);
    _isPaused = new AutoProp<bool>(false);
    _playerGlobalPosition = new AutoProp<Vector3>(Vector3.Zero);
    _cameraBasis = new AutoProp<Basis>(Basis.Identity);
    _numCoinsCollected = new AutoProp<int>(0);
    _numCoinsAtStart = new AutoProp<int>(0);
  }

  internal GameRepo(
    AutoProp<bool> isMouseCaptured,
    AutoProp<bool> isPaused,
    AutoProp<Vector3> playerGlobalPosition,
    AutoProp<Basis> cameraBasis,
    AutoProp<int> numCoinsCollected,
    AutoProp<int> numCoinsAtStart
  )
  {
    _isMouseCaptured = isMouseCaptured;
    _isPaused = isPaused;
    _playerGlobalPosition = playerGlobalPosition;
    _cameraBasis = cameraBasis;
    _numCoinsCollected = numCoinsCollected;
    _numCoinsAtStart = numCoinsAtStart;
  }

  public void SetPlayerGlobalPosition(Vector3 playerGlobalPosition) =>
    _playerGlobalPosition.OnNext(playerGlobalPosition);

  public void SetIsMouseCaptured(bool isMouseCaptured) =>
    _isMouseCaptured.OnNext(isMouseCaptured);

  public void SetCameraBasis(Basis cameraBasis) =>
    _cameraBasis.OnNext(cameraBasis);



  public void OnJump() => Jumped?.Invoke();

  public void Save()
  {
    SaveRequested?.Invoke();
    // TODO: Remove this later
    SaveCompleted?.Invoke();
  }

  public void OnGameEnded(GameOverReason reason)
  {
    _isMouseCaptured.OnNext(false);
    Pause();
    Ended?.Invoke(reason);
  }

  public void Pause()
  {
    _isMouseCaptured.OnNext(false);
    _isPaused.OnNext(true);
  }

  public void Resume()
  {
    _isMouseCaptured.OnNext(true);
    _isPaused.OnNext(false);
  }

  public void OnJumpshroomUsed() => JumpshroomUsed?.Invoke();

  public void SetNumCoinsAtStart(int numCoinsAtStart) =>
    _numCoinsAtStart.OnNext(numCoinsAtStart);

  #region Internals

  protected void Dispose(bool disposing)
  {
    if (!_disposedValue)
    {
      if (disposing)
      {
        // Dispose managed objects.
        _isMouseCaptured.OnCompleted();
        _isMouseCaptured.Dispose();

        _playerGlobalPosition.OnCompleted();
        _playerGlobalPosition.Dispose();

        _cameraBasis.OnCompleted();
        _cameraBasis.Dispose();

        _numCoinsCollected.OnCompleted();
        _numCoinsCollected.Dispose();

        _numCoinsAtStart.OnCompleted();
        _numCoinsAtStart.Dispose();
      }

      _disposedValue = true;
    }
  }

  public void Dispose()
  {
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }

  #endregion Internals
}
