extends ViewPanel

@export var display_scene: PackedScene
@export var button_group: ButtonGroup
var active_tab: Enums.MachineType = Enums.MachineType.SPINNER

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	button_group.pressed.connect(on_active_tab_changed)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func on_active_tab_changed(tabButton: TabButton):
	active_tab = tabButton.machine_type
	populate_machines()
	pass
	
func populate_machines():
	pass