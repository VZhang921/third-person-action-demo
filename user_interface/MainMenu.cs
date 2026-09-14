using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	[Export]
	private Button quitButton, playButton, settingsButton;

	[Export]
	private AnimationPlayer animPlayer;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (quitButton == null) quitButton = GetNode<Button>("Panel/VBoxContainer/Quit");
		if (playButton == null) playButton = GetNode<Button>("Panel/VBoxContainer/Play");
		if (settingsButton == null) settingsButton = GetNode<Button>("Panel/VBoxContainer/Settings");

		quitButton.Pressed += this.ExitGame;

		playButton.Pressed += playButton.ReleaseFocus;
		playButton.Pressed += this.OnPlayClicked;
		
		settingsButton.Pressed += this.OnSettingsClicked;

		if (animPlayer == null) animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
	{
		animPlayer.Play("MenuTransition");
		World root = GetOwner<World>();
		root.PlayerStart();
		

		/*
		if (Player.Instance != null)
		{
			Player.Instance.ProcessMode = ProcessModeEnum.Inherit;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		*/
		
		
	}

	public void OnSettingsClicked()
	{
		Node settingsScene = ResourceLoader.Load<PackedScene>("res://user_interface/settings.tscn").Instantiate();
		AddChild(settingsScene);
	}
}
