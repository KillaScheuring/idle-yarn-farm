using Godot;
using System;

public partial class RecipeInputDisplay : PanelContainer
{
	[Export] public Texture2D Icon;
	[Export] public float RequiredQuantity;
	[Export] public float Quantity;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var icon = GetNode<TextureRect>("Input 1/TextureRect");
		icon.Texture = Icon;
		var label = GetNode<Label>("Input 1/Label");
		label.Text = $"{Quantity}/{RequiredQuantity}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
