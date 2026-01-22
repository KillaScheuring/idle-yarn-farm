using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public class MaterialManager
{
    public Dictionary<string, List<MaterialInfo>> AllMaterials { get; private set; } = new();
    
    public event Action MaterialListChanged;

    public void Initialize()
    {
        LoadMaterialsFromJson();
    }

    private void LoadMaterialsFromJson()
    {
        string jsonPath = "res://static_data.json";
        if (!FileAccess.FileExists(jsonPath)) return;

        using var file = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        string jsonContent = file.GetAsText();

        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;

            AllMaterials["Fibers"] = LoadCategory(root, "Fibers");
            AllMaterials["Yarns"] = LoadCategory(root, "Yarns");
            AllMaterials["Items"] = LoadCategory(root, "Items");
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"Error parsing material: {ex.Message}");
        }
    }

    private List<MaterialInfo> LoadCategory(JsonElement root, string category)
    {
        var list = new List<MaterialInfo>();
        if (!root.TryGetProperty(category, out JsonElement categoryElement)) return list;
        var rng = new RandomNumberGenerator();
        foreach (JsonElement element in categoryElement.EnumerateArray())
        {
            string iconPath = "";
            switch (category)
            {
                case "Fibers": iconPath = "res://assets/icons/Sheep-wool.png"; break;
                case "Yarns": iconPath = "res://assets/icons/yarn.png"; break;
                case "Items": iconPath = "res://assets/icons/item.png"; break;
            }
            list.Add(new MaterialInfo {
                Name = element.GetProperty("name").GetString() ?? "",
                Category = category,
                Amount = element.TryGetProperty("amount", out var amountProp) ? amountProp.GetDouble() : rng.RandiRange(0, 1000000),
                Price = element.TryGetProperty("price", out var priceProp) ? priceProp.GetDouble() : 0,
                Unlock_Cost = element.TryGetProperty("unlock_cost", out var unlockProp) ? unlockProp.GetDouble() : 0,
                Production_Time = element.TryGetProperty("spinning_time", out var spinProp) ? spinProp.GetDouble() : 0,
                Icon_Path = iconPath
            });
        }
        return list;
    }

    public void UpdateAmount(string category, string name, double delta)
    {
        if (!AllMaterials.ContainsKey(category)) return;
        
        var index = AllMaterials[category].FindIndex(r => r.Name == name);
        if (index != -1)
        {
            var resource = AllMaterials[category][index];
            resource.Amount += delta;
            AllMaterials[category][index] = resource; // Update struct in list
            MaterialListChanged?.Invoke();
        }
    }
    
    public MaterialInfo GetResourceByCategoryAndName(string categoryName = null, string resourceName = null)
    {
        return AllMaterials[categoryName].Find(r => r.Name == resourceName);
    }
}