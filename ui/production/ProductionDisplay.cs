using Godot;
using System;

public partial class ProductionDisplay : MarginContainer
{
	public MachineInfo DisplayMachine;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FillDisplay();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if there is a recipe selected
		// Check if there are enough resources to start production
		// If there are enough resources, start the production timer
		
		// Check if production is running
		// Update the production timer
		// Check if production is complete
		// If complete, run completion logic
	}

	private void FillDisplay()
	{
		var recipeType = "yarn";
		var recipeName = DisplayMachine.Recipe;
		var recipe =  GlobalManagement.Instance.Materials.GetResourceByCategoryAndName(recipeType, recipeName).Recipe;
		
		for (var inputIndex = 0; inputIndex < 3; inputIndex++)
		{
			var inputIcon = GetNode<TextureRect>("PanelContainer/MarginContainer/VBoxContainer/Recipe Display/HBoxContainer/Input " + (inputIndex + 1) + "/TextureRect");
			var inputFraction = GetNode<Label>("PanelContainer/MarginContainer/VBoxContainer/Recipe Display/HBoxContainer/Input " + (inputIndex + 1) + "/Label");
			
			if (inputIndex >= recipe.Count)
			{
				inputIcon.Texture = GD.Load<Texture2D>("res://assets/icons/Empty-coin.png");
				inputFraction.Text = "";
				continue;
			}
			var inputMaterial = GlobalManagement.Instance.Materials.GetResourceByCategoryAndName(recipe[inputIndex].Type, recipe[inputIndex].Name);
			inputIcon.Texture = GD.Load<Texture2D>(inputMaterial.Icon_Path);
			inputFraction.Text = "200/100";

		}
	}
	
	private void RecipeSelected(string categoryName, string resourceName)
	{
		// Update the ingredients and output resources
	}

	private void StartProduction()
	{
		// If there are enough resources, start the production timer and reduce the number of output resources in the resource management system
		// If there are not enough resources, turn text red and do not start the production timer
	}

	private void CompleteProduction()
	{
		// Add the output resources to the resource management system
	}

	private void CancelProduction()
	{
		// Add ingredients back to the resource management system
		// Clear the output resources and ingredients
	}

	private void OnResourceListChanged()
	{
		
	}
}
