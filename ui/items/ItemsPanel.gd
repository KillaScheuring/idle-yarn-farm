extends ViewPanel

@export var display_scene: PackedScene
@export var button_group: ButtonGroup
var active_tab: Enums.ItemType = Enums.ItemType.FIBER
var selection: Manager.Item


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	button_group.pressed.connect(on_active_tab_changed)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func on_active_tab_changed(tabButton: TabButton):
	active_tab = tabButton.item_type
	populate_items()
	pass
	
func populate_items():
	pass
