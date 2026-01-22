using Godot;
using System.Collections.Generic;
using System.Text.Json;

public struct MaterialInfo
{
    public string Name;
    public string Category;
    public double Amount;
    public double Price;
    public double Unlock_Cost;
    public double Production_Time;
    public string Icon_Path;
}

public struct MachineInfo
{
    public string Name;
    public double Unlock_Cost;
    public double Is_Unlocked;
    public string Recipe;
    public float Last_Updated;
}

public partial class GlobalManagement : Node
{
    [Signal]
    public delegate void CoinsChangedEventHandler(double totalCoins);

    [Signal]
    public delegate void CurrentViewChangedEventHandler(string currentView);

    [Signal]
    public delegate void CurrentMaterialTabChangedEventHandler(string currentMaterialTab);

    [Signal]
    public delegate void SelectedMaterialChangedEventHandler(string materialName);
    
    [Signal]
    public delegate void MaterialListChangedEventHandler();

    [Signal]
    public delegate void CurrentProductionTabChangedEventHandler(string currentProductionTab);
    
    public MaterialManager Materials { get; private set; } = new();
    public static GlobalManagement Instance { get; private set; }
    public double TotalCoins { get; private set; } = 0;
    public string CurrentView { get; set; }
    public string CurrentMaterialTab { get; set; } = "Fibers";
    public string SelectedMaterialName { get; set; }
    public string CurrentProductionTab { get; set; } = "Spinning";
    
    public override void _Ready()
    {
        Materials.Initialize();
        Instance = this;
        Materials.MaterialListChanged += () => EmitSignal(SignalName.MaterialListChanged);
    }

    public void _Process(float delta)
    {
    }

    public void AddCoins(double amount)
    {
        TotalCoins += amount;
        EmitSignal(SignalName.CoinsChanged, TotalCoins);
    }

    public void RemoveCoins(double amount)
    {
        TotalCoins -= amount;
        EmitSignal(SignalName.CoinsChanged, TotalCoins);
    }
    public void AddMaterial(string category, string name, double amount) 
        => Materials.UpdateAmount(category, name, amount);

    public void RemoveMaterial(string category, string name, double amount) 
        => Materials.UpdateAmount(category, name, -amount);

    public void SetCurrentView(string viewName)
    {
        CurrentView = viewName;
        EmitSignal(SignalName.CurrentViewChanged, viewName);
    }

    public void SetCurrentMaterialTab(string tabName)
    {
        CurrentMaterialTab = tabName;
        EmitSignal(SignalName.CurrentMaterialTabChanged, tabName);
    }

    public void SetSelectedMaterial(string materialName)
    {
        SelectedMaterialName = materialName;
        EmitSignal(SignalName.SelectedMaterialChanged, materialName);
    }

    public void SetCurrentProductionTab(string tabName)
    {
        CurrentProductionTab = tabName;
        EmitSignal(SignalName.CurrentProductionTabChanged, tabName);
    }
}