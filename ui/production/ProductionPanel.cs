using Godot;
using System;

public partial class ProductionPanel : ViewPanel
{
	[Export] public string ActiveTab;
	[Export] public PackedScene DisplayScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnActiveTabChanged("Spinning");
		var tabBar = GetNode<HBoxContainer>("Production Panel/VBoxContainer/Tab Bar/HBoxContainer");
		foreach (Node child in tabBar.GetChildren())
		{
			if (child is TabButton button)
			{
				button.ButtonClicked += OnActiveTabChanged;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnActiveTabChanged(string tabName)
	{
		ActiveTab = Utils.Instance.ConvertTabName(tabName);
		UpdateTabButtonStates();
		PopulateMachines();
	}
	private void UpdateTabButtonStates()
	{
		var tabBar = GetNode<HBoxContainer>("Production Panel/VBoxContainer/Tab Bar/HBoxContainer");
		foreach (var child in tabBar.GetChildren())
		{
			if (child is TabButton tabButton)
				tabButton.ButtonPressed = Utils.Instance.ConvertTabName(tabButton.Name) == ActiveTab;
		}
	}
	
	private void PopulateMachines()
	{
		var container = GetNode<VBoxContainer>("Production Panel/VBoxContainer/Production List/PanelContainer/MarginContainer/ScrollContainer/MarginContainer/VBoxContainer");

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for the active tab
		// foreach (Machine machine in GlobalManagement.Instance.Machines)
		// {
		// 	if (DisplayScene?.Instantiate() is MachineDisplay display)
		// 	{
		// 		display.Machine = machine;
		// 		container.AddChild(display);
		// 	}
		// }
	}
}
