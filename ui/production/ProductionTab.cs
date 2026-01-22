using Godot;
using System;

public partial class ProductionTab : VBoxContainer
{
	[Export] public PackedScene DisplayScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the global Autoload instance
		GlobalManagement.Instance.CurrentProductionTabChanged += OnProductionTabChanged;

		// Initial population
		PopulateMachines(GlobalManagement.Instance.CurrentProductionTab);
	}

	private void OnProductionTabChanged(string currentTab)
	{
		PopulateMachines(currentTab);
	}

	private void PopulateMachines(string tabName)
	{
		var container = GetNode<GridContainer>("Machine List/VBoxContainer/PanelContainer/ScrollContainer/MarginContainer/Production Tab");
		var machineManager = GlobalManagement.Instance.Machines;

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for current tab
		foreach (var data in machineManager.AllMachines[tabName])
		{
			GD.Print(data.Name);
			if (DisplayScene?.Instantiate() is ProductionDisplay display)
			{
				display.DisplayMachine = data;
				container.AddChild(display);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
