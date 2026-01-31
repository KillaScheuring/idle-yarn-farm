using Godot;
using Godot.Collections;
using System.IO;

public partial class Item: Resource
{
    public ItemStats Stats;
    public double Quantity;
    public bool Unlocked;
    
    public double RemoveQuantity(double amount)
    {
        Quantity -= amount;
        return Quantity;
    }
    public double AddQuantity(double amount)
    {
        Quantity += amount;
        return Quantity;
    }
}

public partial class Machine : Resource
{
    public MachineStats Stats;
    public Recipe Recipe { get; set;}
    public bool Unlocked;
}

public partial class GlobalManagement : Node
{
    [Signal]
    public delegate void CoinsChangedEventHandler(double totalCoins);

    [Signal]
    public delegate void CurrentViewChangedEventHandler(string currentView);

    [Signal]
    public delegate void MaterialListChangedEventHandler();
    
    [Signal]
    public delegate void MachineListChangedEventHandler();

    public static GlobalManagement Instance { get; private set; }
    public double TotalCoins { get; private set; } = 0;
    public string CurrentView { get; set; }
    public Array<Item> Items { get; private set; }
    public Array<Machine> Machines { get; private set; }
    
    public override void _Ready()
    {
        Items = new Array<Item>();
        LoadItems();
        GD.Print(Items);
        Instance = this;
    }

    public void _Process(float delta)
    {
    }

    private void LoadItems()
    {
         // Loop through files in "items" directory
        string itemsPath = "res://resources/items";
        using var dir = DirAccess.Open(itemsPath);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && fileName.EndsWith(".tres"))
                {
                    string filePath = itemsPath + "/" + fileName;
                    using var file = ResourceLoader.Load(filePath) as ItemStats;
                    if (file != null)
                    {
                        Items.Add(new Item
                        {
                            Stats = file,
                            Quantity = 0,
                            Unlocked = false
                        });
                    }
                }
                fileName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
    }
    
    private void LoadMachines()
    {
        // Loop through files in "items" directory
        string machinesPath = "res://resources/machines";
        using var dir = DirAccess.Open(machinesPath);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && fileName.EndsWith(".tres"))
                {
                    string filePath = machinesPath + "/" + fileName;
                    using var file = ResourceLoader.Load(filePath) as MachineStats;
                    if (file != null)
                    {
                        Machines.Add(new Machine
                        {
                            Stats = file,
                            Unlocked = false
                        });
                    }
                }
                fileName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
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

    public void AddItems(string name, double amount)
    {
        
    }

    public void RemoveItems(string name, double amount)
    {
        
    }

    public Array<Item> GetItemsOfType(ItemType itemType)
    {
        return Items;
    }

    public Array<Machine> GetMachinesOfType(MachineType machineType)
    {
        return Machines;
    }

    public void SetCurrentView(string viewName)
    {
        CurrentView = viewName;
        EmitSignal(SignalName.CurrentViewChanged, viewName);
    }
}