using Godot;
using System;

public partial class PanelCollapseButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect button press
		Pressed += OnButtonPressed;
	}

	private void OnButtonPressed()
	{
		GlobalManagement.Instance.SetCurrentView("");
	}
}
