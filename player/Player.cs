using Godot;
using System;
using System.ComponentModel;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private Node3D cameraPivot;

	[Export(PropertyHint.Range, "0, 0.05")]
	private float CameraSensitivity_H = 0.02f;

	[Export(PropertyHint.Range, "0, 0.05")]
	private float CameraSensitivity_V = 0.02f;

	private Node3D body;
	//private Vector3 rotation;

	private Vector3 maxSpringRotation = new Vector3(Mathf.DegToRad(80), 30, 0);

	private AnimationTree animTree;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		cameraPivot = GetNode<Node3D>("%CameraPivot");
		body = GetNode<Node3D>("Body");
		//rotation = body.Rotation;

		animTree = GetNode<AnimationTree>("AnimationTree");
		
	}

    public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_cancel")) Input.MouseMode = Input.MouseModeEnum.Visible;
		
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			cameraPivot.RotateY(-eventMouseMotion.Relative.X * CameraSensitivity_H);
			//this.RotateY(-eventMouseMotion.Relative.X * CameraSensitivity_H);
			//cameraPivot.RotateX(-eventMouseMotion.Relative.Y * CameraSensitivity_V);

			cameraPivot.RotateObjectLocal(Vector3.Right, -eventMouseMotion.Relative.Y * CameraSensitivity_V);

			cameraPivot.Rotation = cameraPivot.Rotation.Clamp(-maxSpringRotation, maxSpringRotation);
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		direction = direction.Rotated(Vector3.Up, cameraPivot.Rotation.Y);
		
		if (direction != Vector3.Zero)
		{
			float targetYAngle = Mathf.Atan2(direction.X, direction.Z);
			GD.Print("targetYAngle: " + Mathf.RadToDeg(targetYAngle));
			body.Rotation = new Vector3(body.Rotation.X, Mathf.LerpAngle(body.Rotation.Y, targetYAngle, 0.15f), body.Rotation.Z);

			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;

		animTree.Set("parameters/conditions/idle", (IsOnFloor() && inputDir == Vector2.Zero));
		animTree.Set("parameters/conditions/move", (IsOnFloor() && inputDir != Vector2.Zero));

		MoveAndSlide();
	}
}
