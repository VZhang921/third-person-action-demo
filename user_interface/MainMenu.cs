using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	[Export]
	private Button quitButton, playButton, settingsButton;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (quitButton == null) quitButton = GetNode<Button>("Panel/VBoxContainer/Quit");
		if (playButton == null) playButton = GetNode<Button>("Panel/VBoxContainer/Play");
		if (settingsButton == null) settingsButton = GetNode<Button>("Panel/VBoxContainer/Settings");

		quitButton.Pressed += this.ExitGame;
		playButton.Pressed += this.OnPlayClicked;
		settingsButton.Pressed += this.OnSettingsClicked;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ExitGame()
	{
		GetTree().Quit();
	}

	public void OnPlayClicked()
	{ }

	public void OnSettingsClicked()
	{ }
}
