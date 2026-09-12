using Godot;
using System;

public partial class World : Node3D
{
	private Camera3D previewCamera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (previewCamera == null) previewCamera = GetNode<Camera3D>("PreviewCamera");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
