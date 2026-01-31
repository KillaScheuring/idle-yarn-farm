using Godot;
using System;

public partial class TabButton : Button
{
	[Signal]
	public delegate void ButtonClickedEventHandler(string tabName);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += _OnPressed;
	}

	private void _OnPressed()
	{
		EmitSignal(SignalName.ButtonClicked, Name);
	}
}
