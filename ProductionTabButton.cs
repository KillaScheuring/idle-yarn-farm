using Godot;
using System;

public partial class ProductionTabButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var globalManager = GetNode<ResourceManagementSystem>("/root/ResourceManagementSystem");
		globalManager.CurrentProductionTabChanged += OnProductionTabChanged;

		ButtonPressed = globalManager.CurrentProductionTab == Name;

		// Initial check
		OnProductionTabChanged(globalManager.CurrentProductionTab);

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
		var globalManager = GetNode<ResourceManagementSystem>("/root/ResourceManagementSystem");
		globalManager.SetCurrentProductionTab(Name);
	}
}
