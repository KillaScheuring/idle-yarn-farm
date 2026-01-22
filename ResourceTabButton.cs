using Godot;
using System;

public partial class ResourceTabButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var globalManager = GetNode<ResourceManagementSystem>("/root/ResourceManagementSystem");
		globalManager.CurrentResourceTabChanged += OnResourceTabChanged;

		ButtonPressed = globalManager.CurrentResourceTab == Name;

		// Initial check
		OnResourceTabChanged(globalManager.CurrentResourceTab);

		// Connect button press
		Pressed += OnButtonPressed;
		
		Text = Name;
	}

	private void OnResourceTabChanged(string currentTab)
	{
		ButtonPressed = currentTab == Name;
	}

	private void OnButtonPressed()
	{
		var globalManager = GetNode<ResourceManagementSystem>("/root/ResourceManagementSystem");
		globalManager.SetCurrentResourceTab(Name);
	}
}
