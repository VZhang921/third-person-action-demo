using Godot;
using System;

public partial class World : Node3D
{
	private Camera3D previewCamera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (previewCamera == null) previewCamera = GetNode<Camera3D>("PreviewCamera");

		if (previewCamera.Current)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
			Player.Instance.ProcessMode = ProcessModeEnum.Disabled;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerStart()
	{
		Player.Instance.ProcessMode = ProcessModeEnum.Inherit;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		previewCamera.Current = false;
	}
}
