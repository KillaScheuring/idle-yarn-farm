using Godot;
using System;

public partial class ResourceTabButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalManagement.Instance.CurrentMaterialTabChanged += OnResourceTabChanged;

		ButtonPressed = Utils.Instance.ConvertTabName(GlobalManagement.Instance.CurrentMaterialTab) == Name;

		// Initial check
		OnResourceTabChanged(GlobalManagement.Instance.CurrentMaterialTab);

		// Connect button press
		Pressed += OnButtonPressed;
		
		Text = Name;
	}

	private void OnResourceTabChanged(string currentTab)
	{
		ButtonPressed = Utils.Instance.ConvertTabName(currentTab) == Name;
	}

	private void OnButtonPressed()
	{
		GlobalManagement.Instance.SetCurrentMaterialTab(Name);
	}
}
