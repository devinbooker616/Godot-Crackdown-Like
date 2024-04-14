namespace Crackdownlike;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.PowerUps;
using Godot;
using SuperNodes.Types;

public interface IGame : INode3D, IProvide<IGameRepo>;

[SuperNode(typeof(Provider), typeof(Dependent), typeof(AutoNode))]
public partial class Game : Node3D, IGame {
  public override partial void _Notification(int what);

  #region State

  public IGameRepo GameRepo { get; set; } = default!;
  public IGameLogic GameLogic { get; set; } = default!;

  public GameLogic.IBinding GameBinding { get; set; } = default!;

  #endregion State

  #region Nodes

  [Node] public IPlayerCamera PlayerCamera { get; set; } = default!;

  [Node] public IPlayer Player { get; set; } = default!;

  [Node] public IMap Map { get; set; } = default!;
//   [Node] public IInGameUI InGameUi { get; set; } = default!;
//   [Node] public IDeathMenu DeathMenu { get; set; } = default!;
//   [Node] public IWinMenu WinMenu { get; set; } = default!;
//   [Node] public IPauseMenu PauseMenu { get; set; } = default!;

  #endregion Nodes

  #region Provisions

  IGameRepo IProvide<IGameRepo>.Value() => GameRepo;

  #endregion Provisions

  #region Dependencies

  [Dependency] public IAppRepo AppRepo => DependOn<IAppRepo>();

  #endregion Dependencies

  public void Setup() {
    GameRepo = new GameRepo();
    GameLogic = new GameLogic(GameRepo, AppRepo);

    // DeathMenu.TryAgain += OnStart;
    // DeathMenu.MainMenu += OnMainMenu;
    // DeathMenu.TransitionCompleted += OnDeathMenuTransitioned;
    // WinMenu.MainMenu += OnMainMenu;
    // WinMenu.TransitionCompleted += OnWinMenuTransitioned;
    // PauseMenu.MainMenu += OnMainMenu;
    // PauseMenu.Resume += OnResume;
    // PauseMenu.TransitionCompleted += OnPauseMenuTransitioned;
    // PauseMenu.Save += OnPauseMenuSaveRequested;

    Provide();

    // Calling Provide() triggers the Setup/OnResolved on dependent
    // nodes who depend on the values we provide. This means that
    // all nodes registering save managers will have already registered
    // their relevant save managers by now. This is useful when restoring state
    // while loading an existing save file.
  }

  public void OnResolved() {
    GameBinding = GameLogic.Bind();
    GameBinding
    //   .Handle<GameLogic.Output.StartGame>(
    //     _ => {
    //       PlayerCamera.UsePlayerCamera();
    //       InGameUi.Show();
    //     })
      .Handle<GameLogic.Output.SetPauseMode>(
        output => GetTree().Paused = output.IsPaused
      )
      .Handle<GameLogic.Output.CaptureMouse>(
        output => Input.MouseMode = output.IsMouseCaptured
          ? Input.MouseModeEnum.Captured
          : Input.MouseModeEnum.Visible
      );
    //   .Handle<GameLogic.Output.ShowLostScreen>(_ => {
    //     DeathMenu.Show();
    //     DeathMenu.FadeIn();
    //     DeathMenu.Animate();
    //   })
    //   .Handle<GameLogic.Output.ExitLostScreen>(_ => DeathMenu.FadeOut())
    //   .Handle<GameLogic.Output.ShowPauseMenu>(_ => {
    //     PauseMenu.Show();
    //     PauseMenu.FadeIn();
    //   })
    //   .Handle<GameLogic.Output.ShowWonScreen>(_ => {
    //     WinMenu.Show();
    //     WinMenu.FadeIn();
    //   })
    //   .Handle<GameLogic.Output.ExitWonScreen>(_ => WinMenu.FadeOut())
    //   .Handle<GameLogic.Output.ExitPauseMenu>(_ => PauseMenu.FadeOut())
    //   .Handle<GameLogic.Output.HidePauseMenu>(_ => PauseMenu.Hide())
    //   .Handle<GameLogic.Output.ShowPauseSaveOverlay>(
    //     _ => PauseMenu.OnSaveStarted()
    //   )
    //   .Handle<GameLogic.Output.HidePauseSaveOverlay>(
    //     _ => PauseMenu.OnSaveFinished()
    //   );

    // Trigger the first state's OnEnter callbacks so our bindings run.
    // Keeps everything in sync from the moment we start!
    GameLogic.Start();


  }

  public override void _Input(InputEvent @event) {
    if (Input.IsActionJustPressed("ui_cancel")) {
      GameLogic.Input(new GameLogic.Input.PauseButtonPressed());
    }
  }

  public void OnMainMenu() =>
    GameLogic.Input(new GameLogic.Input.GoToMainMenu());

  public void OnResume() =>
    GameLogic.Input(new GameLogic.Input.PauseButtonPressed());

  public void OnStart() =>
    GameLogic.Input(new GameLogic.Input.Start());

  public void OnWinMenuTransitioned() =>
    GameLogic.Input(new GameLogic.Input.WinMenuTransitioned());

  public void OnPauseMenuTransitioned() =>
    GameLogic.Input(new GameLogic.Input.PauseMenuTransitioned());

  public void OnPauseMenuSaveRequested() =>
    GameLogic.Input(new GameLogic.Input.SaveRequested());

  public void OnDeathMenuTransitioned() =>
    GameLogic.Input(new GameLogic.Input.DeathMenuTransitioned());

  public void OnExitTree() {
    // DeathMenu.TryAgain -= OnStart;
    // DeathMenu.MainMenu -= OnMainMenu;
    // DeathMenu.TransitionCompleted -= OnDeathMenuTransitioned;
    // WinMenu.MainMenu -= OnMainMenu;
    // PauseMenu.MainMenu -= OnMainMenu;
    // PauseMenu.Resume -= OnResume;
    // PauseMenu.TransitionCompleted -= OnPauseMenuTransitioned;

    GameLogic.Stop();
    GameBinding.Dispose();
    GameRepo.Dispose();
  }
}
