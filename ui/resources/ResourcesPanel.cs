using Godot;
using System;

public partial class ResourcesPanel : ViewPanel
{
	[Export] public string ActiveTab;
	[Export] public PackedScene ButtonScene;
	public MaterialInfo Selection;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnActiveTabChanged("Fibers");
		var tabBar = GetNode<HBoxContainer>("Resources Panel/VBoxContainer/Tab Bar/HBoxContainer");
		foreach (Node child in tabBar.GetChildren())
		{
			if (child is ResourcesTabButton button)
			{
				button.ButtonClicked += OnActiveTabChanged;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnSelectionChanged(string materialName)
	{
		// Update the sell materials display
		GD.Print($"Selected {materialName}");
		Selection = GlobalManagement.Instance.Materials.GetResourceByCategoryAndName(ActiveTab, materialName);
		var sellMaterials = GetNode<SellMaterials>("Resources Panel/VBoxContainer/Selling Materials");
		sellMaterials.UpdateDisplay(Selection);
		
		// Update the Material button states
		var container = GetNode<VBoxContainer>("Resources Panel/VBoxContainer/Resource List/PanelContainer/MarginContainer/ScrollContainer/MarginContainer/VBoxContainer");
		foreach (var child in container.GetChildren())
		{
			if (child is MaterialButton materialButton)
				materialButton.UpdateStyle(Selection.Name);
		}
	}

	private void OnActiveTabChanged(string tabName)
	{
		ActiveTab = Utils.Instance.ConvertTabName(tabName);
		UpdateTabButtonStates();
		PopulateButtons();
	}
	private void UpdateTabButtonStates()
	{
		var tabBar = GetNode<HBoxContainer>("Resources Panel/VBoxContainer/Tab Bar/HBoxContainer");
		foreach (var child in tabBar.GetChildren())
		{
			if (child is ResourcesTabButton tabButton)
				tabButton.ButtonPressed = Utils.Instance.ConvertTabName(tabButton.Name) == ActiveTab;
		}
	}
	
	private void PopulateButtons()
	{
		var container = GetNode<VBoxContainer>("Resources Panel/VBoxContainer/Resource List/PanelContainer/MarginContainer/ScrollContainer/MarginContainer/VBoxContainer");
		var resourceManager = GlobalManagement.Instance.Materials;

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for the active tab
		foreach (var materialInfo in resourceManager.AllMaterials[ActiveTab])
		{
			if (ButtonScene?.Instantiate() is MaterialButton button)
			{
				button.DisplayMaterial = materialInfo;
				button.SelectionChanged += OnSelectionChanged;
				button.Name = materialInfo.Name;
				container.AddChild(button);
			}
		}
	}
}
