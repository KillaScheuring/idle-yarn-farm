using Godot;
using System;

public partial class ViewPanel : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalManagement.Instance.CurrentViewChanged += OnCurrentViewChanged;
	}

	private void OnCurrentViewChanged(string currentView)
	{
		if (currentView == Name) Show();
		else Hide();
	}
}
