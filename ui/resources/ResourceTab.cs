using Godot;
using System;

public partial class ResourceTab : MarginContainer
{           
    [Export] public PackedScene ButtonScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Get the global Autoload instance
        var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.CurrentResourceTabChanged += OnResourceTabChanged;

		// Initial population
		PopulateButtons(globalManager.CurrentResourceTab);
	}

	private void OnResourceTabChanged(string currentTab)
	{
		PopulateButtons(currentTab);
	}

	private void PopulateButtons(string tabName)
	{
		var container = GetNode<VBoxContainer>("ScrollContainer/MarginContainer/VBoxContainer");
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for current tab
		foreach (var data in globalManager.AllResources[tabName])
		{
			if (ButtonScene?.Instantiate() is ResourceButton button)
			{
				button.DisplayResource = data;
				container.AddChild(button);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
