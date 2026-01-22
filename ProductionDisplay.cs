using Godot;
using System;

public partial class ProductionDisplay : MarginContainer
{
	private string RecipeCategoryName;
	private string RecipeResourceName;
	private double TimeRemaining;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	
	private void RecipeSelected(string categoryName, string resourceName)
	{
		// Update the ingredients and output resources
		RecipeCategoryName = categoryName;
		RecipeResourceName = resourceName;
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
