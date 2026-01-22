using Godot;
using System;

public partial class BottomNavButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalManagement.Instance.CurrentViewChanged += OnCurrentViewChanged;

		ButtonPressed = GlobalManagement.Instance.CurrentMaterialTab == Name;

		// Initial check
		OnCurrentViewChanged(GlobalManagement.Instance.CurrentMaterialTab);

		// Connect button press
		Pressed += OnButtonPressed;
	}

	private void OnCurrentViewChanged(string currentTab)
	{
		ButtonPressed = currentTab == Name;
	}

	private void OnButtonPressed()
	{
		GlobalManagement.Instance.SetCurrentView(Name);
	}
}
