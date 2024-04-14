namespace Crackdownlike;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.PowerUps;
using Godot;
using SuperNodes.Types;

public interface IPlayerModel { }

[SuperNode(typeof(Dependent), typeof(AutoNode))]
public partial class PlayerModel : Node3D {
  public override partial void _Notification(int what);

  #region Dependencies
  [Dependency]
  public IPlayerLogic PlayerLogic => DependOn<IPlayerLogic>();
  #endregion Dependencies

  public PlayerLogic.IBinding PlayerBinding { get; set; } =
    default!;

  #region Nodes
  [Node("%AnimationTree")]
  public IAnimationTree AnimationTree { get; set; } = default!;
  public IAnimationNodeStateMachinePlayback AnimationStateMachine {
    get; set;
  } = default!;
  #endregion Nodes

  public void OnReady() => AnimationStateMachine =
    GodotInterfaces.Adapt<IAnimationNodeStateMachinePlayback>(
      (AnimationNodeStateMachinePlayback)AnimationTree.Get(
      "parameters/playback"
      )
    );

  public void OnResolved() {
    PlayerBinding = PlayerLogic.Bind();

    PlayerBinding
      .Handle<PlayerLogic.Output.Animations.Idle>(
        (output) => AnimationStateMachine.Travel("idle")
      )
      .Handle<PlayerLogic.Output.Animations.Move>(
        (output) => AnimationStateMachine.Travel("moving")
      )
      .Handle<PlayerLogic.Output.Animations.Jump>(
        (output) => AnimationStateMachine.Travel("falling")
      )
      .Handle<PlayerLogic.Output.Animations.Fall>(
        (output) => AnimationStateMachine.Travel("falling")
      )
      .Handle<PlayerLogic.Output.MoveSpeedChanged>(
        (output) => AnimationTree.Set(
          "parameters/main_animations/move/blend_position", output.Speed
        )
      );
  }

  public void OnExitTree() {
    PlayerBinding.Dispose();
  }
}
