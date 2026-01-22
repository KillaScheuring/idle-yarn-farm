using Godot;
using System.Collections.Generic;
using System.Text.Json;

public struct ResourceInfo
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

public partial class GlobalManagementSystem : Node
{
    [Signal]
    public delegate void CoinsChangedEventHandler(double totalCoins);
    [Signal]
    public delegate void CurrentViewChangedEventHandler(string currentView);
    [Signal]
    public delegate void CurrentResourceTabChangedEventHandler(string currentResourceTab);
    [Signal]
    public delegate void ResourceListChangedEventHandler();
    [Signal]
    public delegate void SelectedResourceChangedEventHandler(string resourceName);
    [Signal]
    public delegate void CurrentProductionTabChangedEventHandler(string currentProductionTab);

    public double TotalCoins { get; private set; } = 0;
    
    public string CurrentView { get; set; }
    public string CurrentResourceTab { get; set; } = "Fibers";
    public string SelectedResourceName{ get; set; }
    public string CurrentProductionTab { get; set; } = "Spinning";
    
    // This list will hold the actual live data for your game
    public List<ResourceInfo> AllFibers = new();
    public List<ResourceInfo> AllYarns = new();
    public List<ResourceInfo> AllItems = new();

    public Dictionary<string, List<ResourceInfo>> AllResources = new();
    
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

    public void AddResourceAmount(string resourceName, double amount)
    {
        var resource = AllResources[CurrentResourceTab].Find(r => r.Name == resourceName);
        resource.Amount += amount;
        EmitSignal(SignalName.ResourceListChanged);
    }
    
    public void RemoveResourceAmount(string resourceName, double amount)
    {
        var resource = AllResources[CurrentResourceTab].Find(r => r.Name == resourceName);
        resource.Amount -= amount;
        EmitSignal(SignalName.ResourceListChanged);
    }

    public void SetCurrentView(string viewName)
    {
        CurrentView = viewName;
        EmitSignal(SignalName.CurrentViewChanged, viewName);
    }

    public void SetCurrentResourceTab(string tabName)
    {
        CurrentResourceTab = tabName;
        EmitSignal(SignalName.CurrentResourceTabChanged, tabName);
    }

    public void SetSelectedResource(string resourceName)
    {
        SelectedResourceName = resourceName;
        EmitSignal(SignalName.SelectedResourceChanged, resourceName);
    }

    public ResourceInfo GetResourceByCategoryAndName(string categoryName = null, string resourceName = null)
    {
        if (categoryName != null && resourceName != null)
        {
            return AllResources[categoryName].Find(r => r.Name == resourceName);
        }
        return AllResources[CurrentResourceTab].Find(r => r.Name == SelectedResourceName);
    }

    public void SetCurrentProductionTab(string tabName)
    {
        CurrentProductionTab = tabName;
        EmitSignal(SignalName.CurrentProductionTabChanged, tabName);
    }

    public override void _Ready()
    {
        LoadResourcesFromJson();
        AllResources["Fibers"] = AllFibers;
        AllResources["Yarns"] = AllYarns;
        AllResources["Items"] = AllItems;
    }

    private void LoadResourcesFromJson()
    {
        string jsonPath = "res://static_data.json";

        if (!FileAccess.FileExists(jsonPath))
        {
            GD.PrintErr($"static_data.json not found at {jsonPath}");
            return;
        }

        using var file = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        string jsonContent = file.GetAsText();

        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;

        LoadCategoryFromJson(root, "Fibers", AllFibers);
        LoadCategoryFromJson(root,  "Yarns", AllYarns);
        LoadCategoryFromJson(root,  "Items", AllItems);
    }
    catch (JsonException ex)
    {
        GD.PrintErr($"Error parsing static_data.json: {ex.Message}");
    }
}

    private void LoadCategoryFromJson(JsonElement root, string categoryName, List<ResourceInfo> targetList)
{
    var rng = new RandomNumberGenerator();
    
    if (root.TryGetProperty(categoryName, out JsonElement categoryElement))
    {
        foreach (JsonElement element in categoryElement.EnumerateArray())
        {
            string iconPath = "";
            switch (categoryName)
            {
                case "Fibers": iconPath = "res://assets/icons/Sheep-wool.png"; break;
                case "Yarns": iconPath = "res://assets/icons/yarn.png"; break;
                case "Items": iconPath = "res://assets/icons/item.png"; break;
            }
            var resourceInfo = new ResourceInfo
            {
                Name = element.GetProperty("name").GetString() ?? "",
                Category = categoryName,
                Amount = element.TryGetProperty("amount", out var amountProp) ? amountProp.GetDouble() : rng.RandiRange(0, 1000000),
                Price = element.TryGetProperty("price", out var priceProp) ? priceProp.GetDouble() : 0,
                Unlock_Cost = element.TryGetProperty("unlock_cost", out var unlockProp) ? unlockProp.GetDouble() : 0,
                Production_Time = element.TryGetProperty("spinning_time", out var spinProp) ? spinProp.GetDouble() : 0,
                Icon_Path = iconPath
            };

            targetList.Add(resourceInfo);
        }
        }
    }

    public void _Process(float delta)
    {
        
    }
}
