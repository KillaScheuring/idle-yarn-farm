using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public struct MachineInfo
{
    public string Name;
    public double Unlock_Cost;
    public double Is_Unlocked;
    public string Recipe;
    public double Production_Started;
    public double Last_Updated;
}

public class MachineManager
{
    public Dictionary<string, List<MachineInfo>> AllMachines { get; private set; } = new();

    public event Action MachineListChanged;

    public void Initialize()
    {
        LoadMachinesFromJson();
    }

    private void LoadMachinesFromJson()
    {
        string jsonPath = "res://globals/defaults/machines.json";
        if (!FileAccess.FileExists(jsonPath)) return;

        using var file = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        string jsonContent = file.GetAsText();

        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;

            AllMachines["Spinning"] = LoadCategory(root, "Spinning");
            AllMachines["Crafting"] = LoadCategory(root, "Crafting");
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"Error parsing machine: {ex.Message}");
        }
    }

    private List<MachineInfo> LoadCategory(JsonElement root, string category)
    {
        var list = new List<MachineInfo>();
        if (!root.TryGetProperty(category, out JsonElement categoryElement)) return list;
        var rng = new RandomNumberGenerator();
        foreach (JsonElement element in categoryElement.EnumerateArray())
        {
            list.Add(new MachineInfo
            {
                Name = element.GetProperty("name").GetString() ?? "",
                Unlock_Cost = element.TryGetProperty("unlock_cost", out var unlockProp) ? unlockProp.GetDouble() : 0,
                Is_Unlocked = rng.RandiRange(0, 1000000),
                Recipe = element.GetProperty("recipe").GetString() ?? "",
                Last_Updated = 0
            });
        }

        return list;
    }

    public MachineInfo GetMachineByCategoryAndName(string categoryName = null, string machineName = null)
    {
        if (categoryName == null || machineName == null) return new MachineInfo();
        return AllMachines[categoryName].Find(r => r.Name == machineName);
    }

    public void UpdateMachineProgress()
    {
        foreach (var category in AllMachines.Keys)
        {
            for (int index = 0; index < AllMachines[category].Count; index++)
            {
                var machine = AllMachines[category][index];
                machine.Last_Updated = Time.GetUnixTimeFromSystem();
                AllMachines[category][index] = machine; // Update struct in list
            }
        }
        MachineListChanged?.Invoke();
    }
}