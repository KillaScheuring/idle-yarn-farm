using Godot;
using System;

public partial class ItemsPanel : ViewPanel
{
	[Export] public string ActiveTab;
	[Export] public PackedScene ButtonScene;
	[Export] public ButtonGroup ButtonGroup;
	public Item Selection;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonGroup.Pressed += OnActiveTabChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnActiveTabChanged(BaseButton button)
	{
		ActiveTab = Utils.Instance.ConvertTabName(button.Name);
		PopulateButtons();
	}

	private void UpdateTabButtonStates()
	{
		var tabBar = GetNode<HBoxContainer>("Resources Panel/VBoxContainer/Tab Bar/HBoxContainer");
		foreach (var child in tabBar.GetChildren())
		{
			if (child is TabButton tabButton)
				tabButton.ButtonPressed = Utils.Instance.ConvertTabName(tabButton.Name) == ActiveTab;
		}
	}
	
	private void PopulateButtons()
	{
		var container = GetNode<VBoxContainer>("Resources Panel/VBoxContainer/Resource List/PanelContainer/MarginContainer/ScrollContainer/MarginContainer/VBoxContainer");

		// Clear existing buttons
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}

		// Create new buttons for the active tab
		foreach (var item in GlobalManagement.Instance.Items)
		{
			if (ButtonScene?.Instantiate() is ItemButton button)
			{
				button.Item = item;
				button.Name = item.Stats.Name;
				container.AddChild(button);
			}
		}
	}
}
