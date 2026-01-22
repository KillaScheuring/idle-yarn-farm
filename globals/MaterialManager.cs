using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public struct RecipeInput
{
    public string Type;
    public string Name;
    public double Quantity;
}

public struct MaterialInfo
{
    public string Name;
    public string Category;
    public double Amount;
    public double Price;
    public double Unlock_Cost;
    public double Production_Time;
    public string Icon_Path;
    public List<RecipeInput> Recipe;
}

public class MaterialManager
{
    public Dictionary<string, List<MaterialInfo>> AllMaterials { get; private set; } = new();
    public List<string> Material_Types = ["fiber", "yarn", "item"];
    
    public event Action MaterialListChanged;

    public void Initialize()
    {
        LoadMaterialsFromJson();
    }

    private void LoadMaterialsFromJson()
    {
        string jsonPath = "res://globals/defaults/materials.json";
        if (!FileAccess.FileExists(jsonPath)) return;

        using var file = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        string jsonContent = file.GetAsText();

        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;
            foreach (var materialType in Material_Types)
            {
                AllMaterials[materialType] = LoadCategory(root, materialType);
            }
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
                case "fiber": iconPath = "res://assets/icons/Sheep-wool.png"; break;
                case "yarn": iconPath = "res://assets/icons/yarn.png"; break;
                case "item": iconPath = "res://assets/icons/item.png"; break;
            }
            var recipeInputList = new List<RecipeInput>();
            if (element.TryGetProperty("recipe", out JsonElement recipeElement))
            {
                foreach (var inputMaterial in recipeElement.EnumerateArray())
                {
                    recipeInputList.Add(new RecipeInput
                    {
                        Type = inputMaterial.GetProperty("type").GetString() ?? "",
                        Name = inputMaterial.GetProperty("name").GetString() ?? "",
                        Quantity = inputMaterial.GetProperty("quantity").GetDouble()
                    });
                }
            }
            list.Add(new MaterialInfo {
                Name = element.GetProperty("name").GetString() ?? "",
                Category = category,
                Amount = element.TryGetProperty("amount", out var amountProp) ? amountProp.GetDouble() : rng.RandiRange(0, 1000000),
                Price = element.TryGetProperty("price", out var priceProp) ? priceProp.GetDouble() : 0,
                Unlock_Cost = element.TryGetProperty("unlock_cost", out var unlockProp) ? unlockProp.GetDouble() : 0,
                Production_Time = element.TryGetProperty("spinning_time", out var spinProp) ? spinProp.GetDouble() : 0,
                Icon_Path = iconPath,
                Recipe = recipeInputList
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