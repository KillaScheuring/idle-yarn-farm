using Godot;
using System;

public partial class SellMaterials : PanelContainer
{
	[Signal]
	public delegate void SellClickedEventHandler(double percentageSelected);

	public MaterialInfo Selection;
	public double PercentageSelected = 1;
	private Slider _slider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GlobalManagement.Instance.SelectedMaterialChanged += UpdateDisplay;

		_slider = GetNode<Slider>("Sell Materials/HSplitContainer/HSlider");
		_slider.Value = PercentageSelected;
		_slider.ValueChanged += OnSliderValueChanged;

		var totalPrice = GetNode<Label>("Sell Materials/HSplitContainer/HBoxContainer/VBoxContainer/Total Price");
		totalPrice.Text = "$" + Utils.Instance.ConvertToReadable(Selection.Price * Selection.Amount * PercentageSelected);

		var amount = GetNode<Label>("Sell Materials/HSplitContainer/HBoxContainer/VBoxContainer/Amount");
		amount.Text = Utils.Instance.ConvertToReadable(Selection.Amount * PercentageSelected);

		UpdateDisplay(null);
	}

	public void UpdateDisplay(MaterialInfo? selectedMaterials)
	{
		Selection = selectedMaterials ?? new MaterialInfo();
		var selected = GetNode<MarginContainer>("Sell Materials");
		var none = GetNode<MarginContainer>("None Selected");
		if (selectedMaterials != null)
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
	}

	private void OnSellButtonPressed()
    {
        double totalToEarn = Selection.Price * Selection.Amount * PercentageSelected;
        
        GlobalManagement.Instance.AddCoins(totalToEarn);
        GlobalManagement.Instance.RemoveMaterial(Selection.Category, Selection.Name, Selection.Amount * PercentageSelected);
        
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void _process(float delta)
	{
		var totalPrice = GetNode<Label>("Sell Materials/HSplitContainer/HBoxContainer/VBoxContainer/Total Price");
		totalPrice.Text = "$" + Utils.Instance.ConvertToReadable(Selection.Price * Selection.Amount * PercentageSelected);

		var amount = GetNode<Label>("Sell Materials/HSplitContainer/HBoxContainer/VBoxContainer/Amount");
		amount.Text = Utils.Instance.ConvertToReadable(Selection.Amount * PercentageSelected);
	}
}

