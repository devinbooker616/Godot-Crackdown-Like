// using Godot;
// using System;
// using System.Numerics;

// public partial class CameraController : Node3D
// {

// 	[Export]
// 	public int Sensitivty = 5;
// 	Character character;
// 	// Called when the node enters the scene tree for the first time.
// 	public override void _Ready()
// 	{
// 		Input.MouseMode = Input.MouseModeEnum.Captured;
// 		character = GetTree().GetNodesInGroup("Player")[0] as Character;
// 	}

// 	// Called every frame. 'delta' is the elapsed time since the previous frame.
// 	public override void _Process(double delta)
// 	{
		
// 		GlobalPosition = character.GlobalPosition;
// 		GetNode<Camera3D>("SpringArm3D/Camera3D").LookAt(character.GetNode<Node3D>("LookAt").GlobalPosition);

// 	}

//     public override void _Input(InputEvent @event)
//     {
//         base._Input(@event);
// 		if (@event is InputEventMouseMotion) 
// 		{
// 			InputEventMouseMotion motion = (InputEventMouseMotion) @event;
// 			Rotation = new Godot.Vector3
// 			(
// 				Mathf.Clamp(Rotation.X - motion.Relative.Y / 1000 * Sensitivty, -1, .5f), 
// 				Rotation.Y - motion.Relative.X / 1000 * Sensitivty, 
// 				0
// 			);
// 		}
//     }
// }
