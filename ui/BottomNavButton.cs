using Godot;
using System;

public partial class BottomNavButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.CurrentViewChanged += OnCurrentViewChanged;

		ButtonPressed = globalManager.CurrentResourceTab == Name;

		// Initial check
		OnCurrentViewChanged(globalManager.CurrentResourceTab);

		// Connect button press
		Pressed += OnButtonPressed;
	}

	private void OnCurrentViewChanged(string currentTab)
	{
		ButtonPressed = currentTab == Name;
	}

	private void OnButtonPressed()
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.SetCurrentView(Name);
	}
}
