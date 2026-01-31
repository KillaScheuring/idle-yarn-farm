// namespace NewGameProject.resources;
using Godot;
using System.Collections.Generic;
using Godot.Collections;

public enum ItemType { Fiber, Yarn, Item }
[GlobalClass]
public partial class ItemStats(string name, ItemType itemType): Resource
{
    [Export] public string Name { get; set; } = name;
    [Export] public ItemType ItemType { get; set; } = itemType;
    public ItemStats() : this("", ItemType.Fiber) {}
}