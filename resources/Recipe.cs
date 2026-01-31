using Godot;
using Godot.Collections;

public partial class Recipe: Resource
{
    [Export] public Dictionary<ItemStats, double> Inputs;
    [Export] public ItemStats Output;
}