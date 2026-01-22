using Godot;
using System.Collections.Generic;

public partial class Utils : Node
{
    public static Utils Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public string ConvertToReadable(double value)
    {
        // Quadrillion
        if (value >= 1e15)
        {
            return (value / 1e15).ToString("0.00") + "Q";
        }

        // Trillion
        if (value >= 1e12)
        {
            return (value / 1e12).ToString("0.00") + "T";
        }

        // Billion
        if (value >= 1e9)
        {
            return (value / 1e9).ToString("0.00") + "B";
        }

        // Million
        if (value >= 1e6)
        {
            return (value / 1e6).ToString("0.00") + "M";
        }

        // Thousand
        if (value >= 1e3)
        {
            return (value / 1e3).ToString("0.00") + "K";
        }

        return value.ToString("N0");
    }

    public string ConvertTabName(string tabName)
    {
        switch (tabName)
        {
            case "fiber": return "Fibers";
            case "Fibers": return "fiber";
            case "yarn": return "Yarns";
            case "Yarns": return "yarn";
            case "item": return "Items";
            case "Items": return "item";
            case "spinner": return "Spinning";
            case "Spinning": return "spinner";
            case "crafter": return "Crafting";
            case "Crafting": return "crafter";
            default: return tabName;
        }
    }
}