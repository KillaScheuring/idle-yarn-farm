using Godot;
using System;

public partial class Gui : Control
{
	private RichTextLabel _coinLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coinLabel = GetNode<RichTextLabel>("Overlay GUI/Coins");
		var globalManager = GetNode<ResourceManagementSystem>("/root/ResourceManagementSystem");
		// Connect to the signal
		globalManager.CoinsChanged += UpdateCoinDisplay;
        
		// Set initial value
		UpdateCoinDisplay(globalManager.TotalCoins);
	}

	private void UpdateCoinDisplay(double totalCoins)
	{
		_coinLabel.Text = $"[img=80]res://assets/gui/Coin-1.png[/img][b]{totalCoins}[/b]";
	}
}
