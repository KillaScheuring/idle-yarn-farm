using Godot;
using System;

public partial class ProductionTabButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalManagement.Instance.CurrentProductionTabChanged += OnProductionTabChanged;

		ButtonPressed = GlobalManagement.Instance.CurrentProductionTab == Name;

		// Initial check
		OnProductionTabChanged(GlobalManagement.Instance.CurrentProductionTab);

		// Connect button press
		Pressed += OnButtonPressed;
		
		Text = Name;
	}

	private void OnProductionTabChanged(string currentTab)
	{
		ButtonPressed = currentTab == Name;
	}

	private void OnButtonPressed()
	{
		GlobalManagement.Instance.SetCurrentProductionTab(Name);
	}
}
