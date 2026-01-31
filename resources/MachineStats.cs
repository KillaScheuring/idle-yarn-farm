using Godot;
using Godot.Collections;
public enum MachineType { Spinner, Crafter }
[GlobalClass]
public partial class MachineStats(MachineType machineType): Resource
{
    [Export] public MachineType MachineType { get; set; } = machineType;
}