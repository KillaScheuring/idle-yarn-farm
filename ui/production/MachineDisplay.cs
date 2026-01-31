using Godot;
using System;
using System.Collections.Generic;

public partial class MachineDisplay : MarginContainer
{
	public Machine Machine;
	private FoldableContainer _foldableContainer;
	private TextureRect _outputIcon;
	private ProgressBar _progressBar;
	private Button _cancelButton;
	private HBoxContainer _recipeContainer;
	private Timer _productionTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FillDisplay();
		
		var foldableGroup = GD.Load<FoldableGroup>("res://ui/production/machineFolableGroup.tres");
		_foldableContainer = GetNode<FoldableContainer>("PanelContainer/MarginContainer/VBoxContainer/Recipe Display/FoldableContainer");
		_foldableContainer.FoldableGroup = foldableGroup;
		_foldableContainer.Folded = true;
		
		_productionTimer = GetNode<Timer>("Timer");
		_progressBar = GetNode<ProgressBar>("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/AspectRatioContainer/ProgressBar");
		_outputIcon = GetNode<TextureRect>("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/PanelContainer/Output Icon");
		_cancelButton = GetNode<Button>("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/CenterContainer2/Cancel Button");
		_recipeContainer = GetNode<HBoxContainer>("PanelContainer/MarginContainer/VBoxContainer/Recipe Display/FoldableContainer/MarginContainer/HBoxContainer2");
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
		// var recipeType = "yarn";
		// var recipeName = DisplayMachine.Recipe;
		// var recipe =  GlobalManagement.Instance.Materials.GetResourceByCategoryAndName(recipeType, recipeName).Recipe;
		
		for (var inputIndex = 0; inputIndex < 3; inputIndex++)
		{
			var currentRecipeInput = GetNode <RecipeInputDisplay>(
				"PanelContainer/MarginContainer/VBoxContainer/Recipe Display/FoldableContainer/MarginContainer/HBoxContainer2/Input " +
				(inputIndex + 1));
			// if (inputIndex >= DisplayMachine.Recipe.Count)
			// {
			// 	currentRecipeInput.Icon = GD.Load<Texture2D>("res://assets/icons/Empty-coin.png");
			// 	currentRecipeInput.Quantity = 0;
			// 	currentRecipeInput.RequiredQuantity = 0;
			// 	continue;
			// }
			// var inputMaterial = DisplayMachine.Recipe[inputIndex].Material;
			// // currentRecipeInput.Icon = GD.Load<Texture2D>(inputMaterial.Icon_Path);
			// // currentRecipeInput.Quantity = inputMaterial.Amount;
			// currentRecipeInput.RequiredQuantity = DisplayMachine.Recipe[inputIndex].Quantity;
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
