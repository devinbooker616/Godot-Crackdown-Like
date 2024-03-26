using Godot;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;


public partial class camera_controller : Node
{

	private Node3D yawNode; 
	private Node3D pitchNode; 
	private Camera3D camera;


	private Godot.Vector3 targetRotation;
	

	public Godot.Vector3 rotation;
	public float yaw = 0f; 
	public float pitch = 0f;

	public float yawSensitivity = 0.07f; 
	public float pitchSensitivity = 0.07f; 

	public float yawAcceleration = 1f; 
	public float pitchAcceleration = 1f; 

	public float pitchMax = 75f;
	public float pitchMin = -55f;

	
	[ExportCategory("Rotation Variables")]
    [Export] public float VerticalRecoil { get; set; } = 2.0f;
    [Export] public float RotationSpeed { get; set; } = 0.3f;
	[Export] public float CameraActualRotationSpeed { get; set; } = 20;
    [Export] public float ArmsActualRotationSpeed { get; set; } = 12;
    [Export] public float VerticalRotationLimit { get; set; } = 80;


	public Godot.Vector3 positionOffset = new Godot.Vector3(0, 1.3f, 0);
	public Godot.Vector3 positionOffsetTarget = new Godot.Vector3(0, 1.3f, 0);
	public override void _Input(InputEvent @event)
	{
		
		if(@event is InputEventMouseMotion mouseEvent) 
		{
			
			// yaw += -mouseEvent.Relative.X * yawSensitivity; 
		
			// pitch += mouseEvent.Relative.Y * pitchSensitivity; 

			targetRotation = new Godot.Vector3(
				Mathf.Clamp((1 * mouseEvent.Relative.Y * RotationSpeed) + targetRotation.X, -VerticalRotationLimit, VerticalRotationLimit),
				Mathf.Wrap((1 * mouseEvent.Relative.X * RotationSpeed) + targetRotation.Y, 0, 360),
				0);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
		

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
		
	

		var yawNode = GetNode<Node3D>("CamYaw");
		// var camera = GetNode<Camera3D>("Camera3D");

		
		yawNode.Rotation = new Godot.Vector3(
			Mathf.LerpAngle(yawNode.Rotation.X, Mathf.DegToRad(targetRotation.X), CameraActualRotationSpeed * (float)delta),
            Mathf.LerpAngle(yawNode.Rotation.Y, Mathf.DegToRad(targetRotation.Y), CameraActualRotationSpeed * (float)delta),
			0);
		// rotation.Y = Mathf.Lerp(yawNode.RotationDegrees.Y, yaw, yawAcceleration * (float)delta);
		// yawNode.RotateY(rotation.Y);

		// rotation.X = Mathf.Lerp(pitchNode.RotationDegrees.X, pitch, pitchAcceleration * (float)delta);
		// pitchNode.RotateX(rotation.X);
	}
	
}
