using Godot;
using System;

public partial class SellResources : PanelContainer
{
	[Signal]
	public delegate void PercentageChangedEventHandler(double newValue);

	[Signal]
	public delegate void SellClickedEventHandler(double percentageSelected);

	public double PercentageSelected = 1;
	private Slider _slider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var utils = GetNode<Utils>("/root/Utils");
		
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.SelectedResourceChanged += UpdateDisplay;

		var selectedResource = globalManager.GetResourceByCategoryAndName();

		var slider = GetNode<Slider>("Sell Resource/HSplitContainer/HSlider");
		slider.Value = PercentageSelected;
		slider.ValueChanged += OnSliderValueChanged;

		var totalPrice = GetNode<Label>("Sell Resource/HSplitContainer/HBoxContainer/VBoxContainer/Total Price");
		totalPrice.Text = "$" + utils.ConvertToReadable(selectedResource.Price * selectedResource.Amount * PercentageSelected);

		var amount = GetNode<Label>("Sell Resource/HSplitContainer/HBoxContainer/VBoxContainer/Amount");
		amount.Text = utils.ConvertToReadable(selectedResource.Amount * PercentageSelected);

		UpdateDisplay("");
	}

	private void UpdateDisplay(string selectedResource)
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		var selected = GetNode<MarginContainer>("Sell Resource");
		var none = GetNode<MarginContainer>("None Selected");
		if (selectedResource.Length > 0)
		{
			none.Hide();
			selected.Show();
		}
		else
		{
			none.Show();
			selected.Hide();
		}
	}

	private void OnSliderValueChanged(double value)
	{
		PercentageSelected = value;

		// Update UI elements here if needed (like total price)

		EmitSignal(SignalName.PercentageChanged, value);
	}

	private void OnSellButtonPressed()
    {
        var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
        var selectedResource = globalManager.GetResourceByCategoryAndName();
        double totalToEarn = selectedResource.Price * selectedResource.Amount * PercentageSelected;
        
        globalManager.AddCoins(totalToEarn);
        globalManager.RemoveResourceAmount(globalManager.SelectedResourceName, selectedResource.Amount * PercentageSelected);
        
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void _process(float delta)
	{
		var utils = GetNode<Utils>("/root/Utils");
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		var selectedResource = globalManager.GetResourceByCategoryAndName();

		var totalPrice = GetNode<Label>("Sell Resource/HSplitContainer/HBoxContainer/VBoxContainer/Total Price");
		totalPrice.Text = "$" + utils.ConvertToReadable(selectedResource.Price * selectedResource.Amount * PercentageSelected);

		var amount = GetNode<Label>("Sell Resource/HSplitContainer/HBoxContainer/VBoxContainer/Amount");
		amount.Text = utils.ConvertToReadable(selectedResource.Amount * PercentageSelected);
	}
}

