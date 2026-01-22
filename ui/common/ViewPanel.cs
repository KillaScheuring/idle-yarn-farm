using Godot;
using System;

public partial class ViewPanel : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.CurrentViewChanged += OnCurrentViewChanged;
	}

	private void OnCurrentViewChanged(string currentView)
	{
		if (currentView == Name) Show();
		else Hide();
	}
}
