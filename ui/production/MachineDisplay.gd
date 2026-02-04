extends MarginContainer

var machine: Manager.Machine
var output_icon: TextureRect
var progress_bar: ProgressBar
var inputs_container: HBoxContainer

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	output_icon = get_node("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/PanelContainer/Output Icon")
	progress_bar = get_node("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/AspectRatioContainer/ProgressBar")
	inputs_container = get_node("PanelContainer/MarginContainer/VBoxContainer/Recipe Display/FoldableContainer/MarginContainer/HBoxContainer2")
	
	var cancel_button: Button = get_node("PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/HBoxContainer/CenterContainer2/Cancel Button")
	cancel_button.pressed.connect(clear_recipe)
	
	render_machine()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func render_machine():
	for input in range(0, 3):
		continue
	pass
	
func clear_recipe():
	pass
