using Godot;
using System.Collections.Generic;

public partial class Utils : Node
{
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
}