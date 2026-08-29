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

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		cameraPivot = GetNode<Node3D>("%CameraPivot");
	}

    public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_cancel")) Input.MouseMode = Input.MouseModeEnum.Visible;
		
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			cameraPivot.RotateY(-eventMouseMotion.Relative.X * CameraSensitivity_H);
			cameraPivot.RotateX(-eventMouseMotion.Relative.Y * CameraSensitivity_V);
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
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
