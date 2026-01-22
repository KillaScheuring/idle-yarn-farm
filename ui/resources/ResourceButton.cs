using Godot;
using System;

public partial class ResourceButton : PanelContainer
{
	
	public ResourceInfo DisplayResource;

	private StyleBoxFlat _defaultStyle;
	private StyleBoxFlat _selectedStyle;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.SelectedResourceChanged += UpdateStyle;
		
        // Connect the GuiInput signal to a local handler
        GuiInput += OnGuiInput;

		var utils = GetNode<Utils>("/root/Utils");
		
		var texture = GD.Load<Texture2D>(DisplayResource.Icon_Path);
		var icon = GetNode<TextureRect>("MarginContainer/HBoxContainer/Icon");
		icon.Texture = texture;
		
		var label = GetNode<Label>("MarginContainer/HBoxContainer/Label");
		label.Text = DisplayResource.Name;

		var amount = GetNode<Label>("MarginContainer/HBoxContainer/Amount");
		amount.Text = utils.ConvertToReadable(DisplayResource.Amount);
		
		var price = GetNode<Label>("MarginContainer/HBoxContainer/Price");
		price.Text = "$" + utils.ConvertToReadable(DisplayResource.Price);

		_defaultStyle = GD.Load<StyleBoxFlat>("res://ui/resources/unSelectedResource.tres");
		AddThemeStyleboxOverride("panel", _defaultStyle);

		_selectedStyle = GD.Load<StyleBoxFlat>("res://ui/resources/selectedResource.tres");
	}

        private void OnGuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                OnButtonPressed();
            }
        }

    	private void UpdateStyle(string resourceName)
	{
		if (resourceName == DisplayResource.Name)
		{
			AddThemeStyleboxOverride("panel", _selectedStyle);
		}
		else
		{
			AddThemeStyleboxOverride("panel", _defaultStyle);
		}
	}

	private void OnButtonPressed()
	{
		var globalManager = GetNode<GlobalManagementSystem>("/root/GlobalManagementSystem");
		globalManager.SetSelectedResource(DisplayResource.Name);
	}
}
