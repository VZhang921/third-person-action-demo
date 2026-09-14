using Godot;
using System;

public partial class Settings : Control
{
	[Export]
	private Button closeBtn;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (closeBtn == null) closeBtn = GetNode<Button>("ColorRect/CloseButton");
		closeBtn.Pressed += OnSettingsClose;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnSettingsClose()
	{
		this.QueueFree();
	}
}
