using Godot;
using System;

public partial class World : Node3D
{
	private Camera3D previewCamera;
    
	[Export]
	private AudioStreamPlayer worldMusic;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (previewCamera == null) previewCamera = GetNode<Camera3D>("PreviewCamera");
		if (worldMusic == null) worldMusic = GetNode<AudioStreamPlayer>("WorldMusic");

		if (previewCamera.Current)
		{
			GD.Print("World._Ready()");
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
		worldMusic.Play();
		previewCamera.Current = false;
		Player.Instance.ProcessMode = ProcessModeEnum.Inherit;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public void TriggerWorldMusic()
	{
		worldMusic.Play();
		previewCamera.Current = false;
		Player.Instance.ProcessMode = ProcessModeEnum.Inherit;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GD.Print("About to start music.");
	}
}
