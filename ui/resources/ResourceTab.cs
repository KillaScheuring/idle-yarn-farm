using Godot;
using System;

public partial class ResourceTab : MarginContainer
{           
    [Export] public PackedScene ButtonScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Get the global Autoload instance
        GlobalManagement.Instance.CurrentMaterialTabChanged += OnResourceTabChanged;

		// Initial population
		PopulateButtons(GlobalManagement.Instance.CurrentMaterialTab);
	}

	private void OnResourceTabChanged(string currentTab)
	{
		PopulateButtons(currentTab);
	}

	private void PopulateButtons(string tabName)
	{
		var container = GetNode<VBoxContainer>("ScrollContainer/MarginContainer/VBoxContainer");
		var resourceManager = GlobalManagement.Instance.Materials;

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for current tab
		foreach (var data in resourceManager.AllMaterials[tabName])
		{
			if (ButtonScene?.Instantiate() is ResourceButton button)
			{
				button.DisplayMaterial = data;
				container.AddChild(button);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
