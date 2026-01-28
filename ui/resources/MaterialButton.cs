using Godot;
using System;

public partial class MaterialButton : PanelContainer
{
    [Signal]
    public delegate void SelectionChangedEventHandler(string materialName);
    
    public MaterialInfo DisplayMaterial;

    private StyleBoxFlat _defaultStyle;
    private StyleBoxFlat _selectedStyle;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Connect the GuiInput signal to a local handler
        GuiInput += OnGuiInput;

        var utils = GetNode<Utils>("/root/Utils");

        var texture = GD.Load<Texture2D>(DisplayMaterial.Icon_Path);
        var icon = GetNode<TextureRect>("MarginContainer/HBoxContainer/Icon");
        icon.Texture = texture;

        var label = GetNode<Label>("MarginContainer/HBoxContainer/Label");
        label.Text = DisplayMaterial.Name;

        var amount = GetNode<Label>("MarginContainer/HBoxContainer/Amount");
        amount.Text = utils.ConvertToReadable(DisplayMaterial.Amount);

        var price = GetNode<Label>("MarginContainer/HBoxContainer/Price");
        price.Text = "$" + utils.ConvertToReadable(DisplayMaterial.Price);

        _defaultStyle = GD.Load<StyleBoxFlat>("res://ui/resources/unSelectedResource.tres");
        AddThemeStyleboxOverride("panel", _defaultStyle);

        _selectedStyle = GD.Load<StyleBoxFlat>("res://ui/resources/selectedResource.tres");
    }
    
    private void OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            EmitSignal(SignalName.SelectionChanged,  DisplayMaterial.Name);
        }
    }

    public void UpdateStyle(string materialName)
    {
        if (materialName == DisplayMaterial.Name)
        {
            AddThemeStyleboxOverride("panel", _selectedStyle);
        }
        else
        {
            AddThemeStyleboxOverride("panel", _defaultStyle);
        }
    }
}